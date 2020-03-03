ECHO Require Python, 7Zip, PERL and NASM in PATH

SET PATH=D:\Program Files (x86)\7-Zip;C:\Perl64\bin;D:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\VC\Tools\MSVC\14.16.27023\bin\Hostx64\x64;%PATH% 
SET FILENAME=openssl-OpenSSL_1_1_1d.tar.gz
SET VCVARPATH="D:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\VC\Auxiliary\Build\vcvarsall.bat"

Rem Set env -----------------------------------------
@pushd "%~dp0"
call %VCVARPATH% %1
@popd

where /q 7z.exe
IF ERRORLEVEL 1 (
    ECHO The application "7z.exe" is missing. Ensure to add/install it to the PATH in beginning of this script, check SET PATH
    PAUSE
    EXIT /B
)

where /q perl.exe
IF ERRORLEVEL 1 (
    ECHO The application "perl.exe" is missing. Ensure to add/install it to the PATH in beginning of this script, check SET PATH
    PAUSE
    EXIT /B
)

where /q nmake.exe
IF ERRORLEVEL 1 (
    ECHO The application "nmake.exe" is missing. Ensure to add/install it to the PATH in beginning of this script, check SET PATH
    PAUSE
    EXIT /B
)

where /q python.exe
IF ERRORLEVEL 1 (
    ECHO The application "python.exe" [shortcut of python] is missing. Ensure to add/install it to the PATH in beginning of this script, check SET PATH
    PAUSE
    EXIT /B
)

Rem Launch compilation -----------------------------------------

python CompileOpenSSL.py -f %FILENAME% -a %1 -v 150