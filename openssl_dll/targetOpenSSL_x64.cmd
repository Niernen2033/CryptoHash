@pushd "%~dp0"
call %1% amd64
@popd

python build_engine.py -a x64 -f %2 -d %3