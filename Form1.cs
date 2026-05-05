namespace ServerTimeSyncMaster
{
    using System;
    using System.Collections.ObjectModel;
    using System.Management.Automation; // PowerShell操作用
    using System.Management.Automation.Runspaces;
    using System.Security; // SecureString用
    using System.Windows.Forms;
    using System.IO;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnScanAll_Click(object sender, EventArgs e)
        {
            string user = txtAdminUser.Text;
            string pass = txtAdminPass.Text;

            rtbLog.Clear();
            rtbLog.AppendText("一括スキャンを開始します...\n");

            // 表の全行をループで回す
            foreach (DataGridViewRow row in dgvServers.Rows)
            {
                // 新規行(一番下の空行)はスキップ
                if (row.IsNewRow) continue;

                string target = row.Cells[0].Value?.ToString();
                int rowIndex = row.Index;

                if (!string.IsNullOrEmpty(target))
                {
                    rtbLog.AppendText($"{target} に接続を試行します...\n");
                    // 以前作った非同期メソッドを呼び出す
                    await ExecuteRemoteCommandAsync(target, user, pass, "Get-Date", rowIndex);
                }
            }

            rtbLog.AppendText("すべての処理が完了しました。\n");
        }

        private async Task ExecuteRemoteCommandAsync(string computerName, string user, string pass, string command, int rowIndex)
        {
            // パスワード処理
            SecureString securePass = new SecureString();
            foreach (char c in pass) securePass.AppendChar(c);
            PSCredential credential = new PSCredential(user, securePass);

            // 空のセッション状態を作成し、リモート実行に必要な機能だけを追加する
            InitialSessionState iss = InitialSessionState.Create();

            // リモート接続に必要なコマンド(Invoke-Command)を明示的に許可
            iss.Commands.Add(new SessionStateCmdletEntry("Invoke-Command", typeof(Microsoft.PowerShell.Commands.InvokeCommandCommand), null));

            // 言語モードを「制限なし」に設定する
            iss.LanguageMode = PSLanguageMode.FullLanguage;

            // Task.Run を使って、重い処理をバックグラウンド(別スレッド)で実行します
            await Task.Run(() =>
            {
                // localhost の場合は PowerShell を介さず直接計算する
                if (computerName.ToLower() == "localhost")
                {
                    this.Invoke(new Action(() =>
                    {
                        DateTime serverTime = DateTime.Now; // 自分の時刻をそのまま使う
                        DateTime localTime = DateTime.Now;
                        double diffSeconds = 0.00; // 自分自身なのでズレは0

                        dgvServers.Rows[rowIndex].Cells[0].Value = computerName;
                        dgvServers.Rows[rowIndex].Cells[1].Value = serverTime.ToString("yyyy/MM/dd HH:mm:ss");
                        dgvServers.Rows[rowIndex].Cells[2].Value = diffSeconds;
                        dgvServers.Rows[rowIndex].Cells[3].Value = "正常(Local)";
                        dgvServers.Rows[rowIndex].Cells[3].Style.ForeColor = Color.Green;

                        rtbLog.AppendText("localhost のため、ローカルクロックから取得しました。\n");
                    }));
                    return; // ここで処理を終了し、下の PowerShell 実行へは行かない
                }

                using (PowerShell ps = PowerShell.Create(iss))
                {
                    // 実行方法を「AddScript」に集約して、よりシンプルに投げます
                    string script = $"Invoke-Command -ComputerName '{computerName}' -Credential $args[0] -ScriptBlock {{ {command} }}";
                    ps.AddScript(script);
                    ps.AddArgument(credential);

                    try
                    {
                        Collection<PSObject> results = ps.Invoke();

                        this.Invoke(new Action(() =>
                        {
                            // 詳細なエラー(HadErrors)もチェックするように強化
                            if (ps.HadErrors)
                            {
                                foreach (var err in ps.Streams.Error)
                                {
                                    rtbLog.AppendText($"通信エラー: {err}\n");
                                }
                            }

                            if (results.Count == 0 && !ps.HadErrors)
                            {
                                rtbLog.AppendText("結果が空でした（セッションは成功しましたが戻り値がありません）。\n");
                            }
                            else
                            {
                                foreach (var obj in results)
                                {
                                    // 時刻をパース(読み取り)
                                    if (DateTime.TryParse(obj.ToString(), out DateTime serverTime))
                                    {
                                        DateTime localTime = DateTime.Now; // 自分のPCの時刻

                                        // 差分(ズレ)を計算
                                        TimeSpan diff = serverTime - localTime;
                                        double diffSeconds = Math.Round(diff.TotalSeconds, 2); // 小数点2位で丸める

                                        // 表(DataGridView)の各セルに値をセット
                                        dgvServers.Rows[rowIndex].Cells[0].Value = computerName; // 端末名/IP
                                        dgvServers.Rows[rowIndex].Cells[1].Value = serverTime.ToString("yyyy/MM/dd HH:mm:ss"); // 現在の時刻
                                        dgvServers.Rows[rowIndex].Cells[2].Value = diffSeconds; // ズレ(秒)

                                        // 状態の判定と色の変更
                                        if (Math.Abs(diffSeconds) < 1.0)
                                        {
                                            dgvServers.Rows[rowIndex].Cells[3].Value = "正常";
                                            dgvServers.Rows[rowIndex].Cells[3].Style.ForeColor = Color.Green;
                                        }
                                        else
                                        {
                                            dgvServers.Rows[rowIndex].Cells[3].Value = "要同期";
                                            dgvServers.Rows[rowIndex].Cells[3].Style.ForeColor = Color.Red;
                                        }

                                        rtbLog.AppendText($"{computerName} の時刻を取得しました。\n");
                                    }
                                    else
                                    {
                                        rtbLog.AppendText("時刻の形式が正しくありませんでした。\n");
                                    }
                                }
                            }
                            rtbLog.AppendText("処理が完了しました。\n");
                        }));
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            dgvServers.Rows[rowIndex].Cells[3].Value = "例外エラー";
                            dgvServers.Rows[rowIndex].Cells[3].Style.ForeColor = Color.Orange;
                            rtbLog.AppendText($"例外エラー: {ex.Message}\n");
                        }));
                    }
                }
            });
        }

        private async void btnSyncSelected_Click(object sender, EventArgs e)
        {
            // DataGridViewで選択されている行があるかチェック
            if (dgvServers.SelectedRows.Count == 0)
            {
                MessageBox.Show("同期したい端末の行を選択してください。");
                return;
            }

            string user = txtAdminUser.Text;
            string pass = txtAdminPass.Text;

            // 選択されたすべての行に対して処理を行う
            foreach (DataGridViewRow row in dgvServers.SelectedRows)
            {
                string target = row.Cells[0].Value?.ToString();
                int rowIndex = row.Index;

                if (string.IsNullOrEmpty(target)) continue;

                rtbLog.AppendText($"{target} の時刻同期を開始します...\n");

                // 時刻同期コマンドを送る
                // スキャンの時と同じメソッドを使い、コマンドだけ「w32tm...」に変える
                await ExecuteRemoteCommandAsync(target, user, pass, "w32tm /resync /force", rowIndex);

                // 同期が終わったら、確認のためにもう一度時刻を取得(スキャン)する
                rtbLog.AppendText($"{target} の再スキャン中...\n");
                await ExecuteRemoteCommandAsync(target, user, pass, "Get-Date", rowIndex);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string csvPath = "terminal.csv";

            if (File.Exists(csvPath))
            {
                string[] lines = File.ReadAllLines(csvPath);
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        // 表に「端末名/IP」だけを入れた行を追加
                        dgvServers.Rows.Add(line.Trim(), "", "", "");
                    }
                }
            }
            else
            {
                rtbLog.AppendText("terminal.csv が見つかりません。新規に作成してください。\n");
            }
        }

        private async void btnScanSelected_Click(object sender, EventArgs e)
        {
            // 選択されている行があるかチェック
            if (dgvServers.SelectedRows.Count == 0)
            {
                MessageBox.Show("スキャンしたい端末の行を選択してください。");
                return;
            }

            string user = txtAdminUser.Text;
            string pass = txtAdminPass.Text;

            rtbLog.AppendText("選択された端末の個別スキャンを開始します...\n");

            // 選択された行をループで処理
            foreach (DataGridViewRow row in dgvServers.SelectedRows)
            {
                // 新規行(空行)はスキップ
                if (row.IsNewRow) continue;

                string target = row.Cells[0].Value?.ToString();
                int rowIndex = row.Index;

                if (!string.IsNullOrEmpty(target))
                {
                    rtbLog.AppendText($"{target} の状態を更新中...\n");
                    // 既存の取得メソッドを呼び出す
                    await ExecuteRemoteCommandAsync(target, user, pass, "Get-Date", rowIndex);
                }
            }

            rtbLog.AppendText("個別スキャンが完了しました。\n");
        }
    }
}
