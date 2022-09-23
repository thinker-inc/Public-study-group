using System.Diagnostics;
using System.Text.RegularExpressions;

public class ToolbeltCallTool
{
    class TdSiteInfo
    {
        public string Name { get; set; }
        public string ApiKey { get; set; }
        public string EndPoint { get; set; }
    }
    public static void Main(string[] args)
    {
        var sites = new TdSiteInfo[] {
            new TdSiteInfo {
                Name = "Thinker",
                ApiKey = "***/*****",
                EndPoint = "https://api.treasuredata.com"
            },
            new TdSiteInfo {
                Name = "Flipdesk",
                ApiKey = "***/*****",
                EndPoint = "https://api.treasuredata.com"
            },
            new TdSiteInfo {
                Name = "Puma",
                ApiKey = "***/*****",
                EndPoint = "https://api.treasuredata.co.jp"
            },
            new TdSiteInfo {
                Name = "Diesel",
                ApiKey = "***/*****",
                EndPoint = "https://api.treasuredata.co.jp"
            },
            new TdSiteInfo {
                Name = "UsenMedia",
                ApiKey = "***/*****",
                EndPoint = "https://api.treasuredata.co.jp"
            },

        };

        foreach (var site in sites)
        {
            Directory.CreateDirectory(site.Name);
            Directory.SetCurrentDirectory(site.Name);
            tdAllWorkflowDownloader(site.ApiKey, site.EndPoint);
            Directory.SetCurrentDirectory("..");    // ディレクトリを戻す
        }
    }

    private static void tdAllWorkflowDownloader(string apikey, string endpoint)
    {
        Process p = new Process();
        //ComSpec(cmd.exe)のパスを取得して、FileNameプロパティに指定
        p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
        //出力を読み取れるようにする
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardInput = false;
        //ウィンドウを表示しないようにする
        p.StartInfo.CreateNoWindow = true;
        //コマンドラインを指定（"/c"は実行後閉じるために必要）
        p.StartInfo.Arguments = @$"/c td -k {apikey} -e {endpoint} wf workflow";
        //起動
        p.Start();
        //出力を読み取る
        string results = p.StandardOutput.ReadToEnd();
        //プロセス終了まで待機する
        //WaitForExitはReadToEndの後である必要がある
        //(親プロセス、子プロセスでブロック防止のため)
        p.WaitForExit();
        p.Close();

        // プロジェクト名選別
        var wfProjects = results.Split("\r\n").Where(x => IsMatch(x, @"^\s\s\s\s") == false).Where(x => x.Length > 0);

        Console.WriteLine(string.Join("\r\n", wfProjects)); // 取得WFプロジェクト名一覧
        Console.WriteLine(); // 改行

        // プロジェクト名毎のダウンロード実施
        foreach (var wf in wfProjects)
        {
            p.StartInfo.Arguments = @$"/c td -k {apikey} -e {endpoint} wf download " + wf;
            p.Start();
            string ret = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();

            Console.WriteLine(ret);
        }
    }

    public static bool IsMatch(string target, string pattern)
    {
        var rx = new Regex(pattern);
        return rx.IsMatch(target);
    }
}