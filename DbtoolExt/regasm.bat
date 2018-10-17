rem https://docs.microsoft.com/ja-jp/dotnet/framework/tools/regasm-exe-assembly-registration-tool
rem レジストリ登録＆tlbファイル生成
rem ↓これは環境に応じて適時変える。regasm.exeがある場所
set SDK_PATH="C:\Windows\Microsoft.NET\Framework\v4.0.30319"
rem ↓これも環境に応じて適時変える。登録するDLLがある場所
set ASM_FILE="D:\OyamadaWorks\src\github\koichiafmi\csharpedu\DbtoolExt\bin\Debug\DbtoolExt.dll"

cd /D %SDK_PATH%
regasm %ASM_FILE% /codebase /tlb

pause
