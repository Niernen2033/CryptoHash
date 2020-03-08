import os
import os.path
import subprocess
import shutil
import sys
import re
import argparse
import json
import ast
import datetime
from enum import Enum

##############################################################
##GLOBALS#####################################################
##############################################################

DEBUG_INFO = True

def printDebug(debug):
    if DEBUG_INFO:
        print("-I- " + debug)
        
def printError(error):
    if DEBUG_INFO:
        print("-E- " + error)


######################################################################
##BuildEngine#########################################################
######################################################################

class BuildEngine:
    def __init__(self):
        self.compile_flags = "-no-asm"
        self.working_dir = os.getcwd()
        
    def run(self, arch, openSSLFile):
        start_time = datetime.datetime.now()
        
        tmp_working_dir = os.getcwd() + "\\obj"
        dirname = openSSLFile.replace(".tar.gz","")
        openssl_tar_file = openSSLFile.replace(".gz","")
        
        printDebug("Configuration: {}".format(arch))
        if arch == "x86":
            src_suffix = "_" + "vs" + "_32"
            arch_flag = "x86"
            openssl_flag = "VC-WIN32"
        else:
            src_suffix = "_" + "vs" + "_64"
            arch_flag = "amd64"
            openssl_flag = "VC-WIN64A"

        dirname_src = dirname + src_suffix
        dirname_bin = dirname + src_suffix + "_build"
        
        #extract the .gz file
        if not os.path.exists(tmp_working_dir + "\\" + openssl_tar_file):
            subprocess.call("7z x -y {} -o{}".format(openSSLFile, tmp_working_dir)) 
        os.chdir(tmp_working_dir)
        
        #delete previous directories
        shutil.rmtree(os.getcwd()+'\\'+dirname_src, ignore_errors=True)


        #extract tar file
        subprocess.call("7z x -y " + openssl_tar_file)
        os.rename(dirname, dirname_src)
        
        #Compile
        try:
            os.chdir(dirname_src)         
            subprocess.call("perl Configure " + openssl_flag + " --prefix=" + os.path.join(tmp_working_dir,dirname_bin) + " " + self.compile_flags,shell=False)
            if(os.path.exists("ms/do_ms.bat")):
                if arch == "x86":
                    subprocess.call("ms\do_ms.bat",shell=False)
                else:
                    subprocess.call("ms\do_win64a.bat",shell=False)
                       
                subprocess.call("nmake -f ms/ntdll.mak",shell=False)
                subprocess.call("nmake -f ms/ntdll.mak install",shell=False)
            else:
                subprocess.call("nmake",shell=False)
                subprocess.call("nmake test",shell=False)
                subprocess.call("nmake install",shell=False)
        except Exception as e:
            printError("{}".format(e))
        finally:
            end_time = datetime.datetime.now()
            total_time = end_time - start_time
            printDebug("Total build time: {}s".format(total_time.total_seconds()))
            os.chdir(tmp_working_dir)
            os.remove(openssl_tar_file)
            os.chdir(self.working_dir)

######################################################################
##Main################################################################
######################################################################


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("-a", "--arch", help="Second argument must be x86 or x64", required=True)
    parser.add_argument("-d", "--debug", help="Enable debug mode", required=False, type=ast.literal_eval, default=True)
    parser.add_argument("-f", "--filename", help="First argument must be the tar.gz file of OpenSSL source", required=True)
    args = parser.parse_args()
    if args.debug == True:
        DEBUG_INFO = True
    else:
        DEBUG_INFO = False

    buildEngine = BuildEngine()
    buildEngine.run(args.arch, args.filename)
         