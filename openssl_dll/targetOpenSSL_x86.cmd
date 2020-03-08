@pushd "%~dp0"
call %1% x86
@popd

python build_engine.py -a x86 -f %2 -d %3