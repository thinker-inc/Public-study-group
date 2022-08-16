using System.Diagnostics;
using System.Text.RegularExpressions;

public class ToolbeltCallTool
{
    public static void Main(string[] args)
    {
        //Processオブジェクトを作成
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
        p.StartInfo.Arguments = @"/c td wf workflow";

        //起動
        p.Start();

        //出力を読み取る
        string results = p.StandardOutput.ReadToEnd();

        //プロセス終了まで待機する
        //WaitForExitはReadToEndの後である必要がある
        //(親プロセス、子プロセスでブロック防止のため)
        p.WaitForExit();
        p.Close();

        var wfProjects = results.Split("\r\n").Where(x => IsMatch(x, @"^\s\s\s\s") == false).Where(x => x.Length>0);
        foreach (var wf in wfProjects)
        {
            //コマンドラインを指定（"/c"は実行後閉じるために必要）
            p.StartInfo.Arguments = @"/c td wf download "+wf;

            //起動
            p.Start();

            //出力を読み取る
            string ret = p.StandardOutput.ReadToEnd();

            //プロセス終了まで待機する
            //WaitForExitはReadToEndの後である必要がある
            //(親プロセス、子プロセスでブロック防止のため)
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