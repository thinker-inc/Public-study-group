﻿using System;
using System.IO;
using System.Text;

//Processオブジェクトを作成
System.Diagnostics.Process p = new System.Diagnostics.Process();

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

//出力された結果を表示
Console.WriteLine(results);

File.WriteAllText(@"c:\home\sample01.txt", results, Encoding.UTF8);

