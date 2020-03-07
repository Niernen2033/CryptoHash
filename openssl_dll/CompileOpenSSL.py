import os
import os.path
import subprocess
import shutil
import sys
import re
import argparse
import json
from enum import Enum

##############################################################
##GLOBALS#####################################################
##############################################################

OPENSSL_FILE_NAME = "openssl-OpenSSL_1_1_1d.tar.gz"
CONFIGURE_FILE_NAME = 'config.cfg'
DEBUG_INFO = True

def printDebug(debug):
    if DEBUG_INFO:
        print("-I- " + debug)
        
def printError(error):
    if DEBUG_INFO:
        print("-E- " + error)
        
def getInputValue(info = ""):
    value = input(info)
    return value

def getIntegerInputValue(info = ""):
    try:
        value = int(getInputValue(info))
    except Exception as e:
        print("Error {}\n".format(e))
        return -1
    return value

######################################################################
##ConfigureEngine#####################################################
######################################################################

class ConfigureOption(Enum):
    EDIT_CONFIG = 1
    CHECK_CONFIG = 2
    PRINT_CONFIG = 3
    RETURN = 4
    
class ConfigureEditOption(Enum):
    EDIT_PATH_7z = 1
    EDIT_PATH_PERL = 2
    EDIT_PATH_NMAKE = 3
    EDIT_VCVARSALL = 4
    EDIT_ARCHITECTURE = 5
    EDIT_VERSION = 6
    RETURN = 7

class ConfigureEngine:
    def __init__(self):
        self._startPATH = os.environ['PATH']
        self.config = {}
    
    def _reloadPATH(self):
        os.environ['PATH'] = self._startPATH
        for path in self.config['paths'].values():
            if path != "":
                printDebug("Adding {} to path".format(path))
                os.environ['PATH'] += ";" + path
    
    def _analysePathsStatus(self, refresh = False):
        if refresh:
            self._reloadPATH()
        if not self._checkIfCmdExists("7z.exe"):
            printError("7z.exe not found")
            return False
        if not self._checkIfCmdExists("perl.exe"):
            printError("perl.exe not found")
            return False
        if not self._checkIfCmdExists("nmake.exe"):
            printError("nmake.exe not found")
            return False
        return True
        
    def _analyseArchitectureStatus(self):
        if self.config['architecture'] != "x86" and self.config['architecture'] != "x64":
            printError("Wrong architecture {}".format(self.config['architecture']))
            return False
            
        return True
        
    def _analyseVersionStatus(self):
        if self.config['version'] != "150" and self.config['version'] != "90" and self.config['version'] != "140":
            printError("Wrong version {}".format(self.config['version']))
            return False
            
        return True
        
    def _analyseVcvarsallStatus(self):
        if not os.path.exists(self.config['vcvarsall']):
            printError("vcvarsall not found in: {}".format(self.config['vcvarsall']))
            return False
            
        return True
        
    def _checkIfCmdExists(self, cmd):
        if(subprocess.call("where " + cmd, shell=True, stdout=subprocess.PIPE, stderr=subprocess.PIPE) == 0):
            return True
        else:
            return False
    
    def checkConfig(self):
        return self._analysePathsStatus(True) and self._analyseVcvarsallStatus() and self._analyseArchitectureStatus() and self._analyseVersionStatus()
        
    def printConfig(self):
        print(self.config)
    
    def _createEmptyConfig(self):
        self.config.clear()
        self.config['paths'] = { '7z': "", 'perl': "", 'nmake': ""}
        self.config['vcvarsall'] = ""
        self.config['architecture'] = "x32"
        self.config['version'] = "150"
        
    def _save(self, fileName = CONFIGURE_FILE_NAME):
        try:
            with open(fileName, 'w') as json_file:
                json.dump(self.config, json_file)
        except Exception as e:
            printError("{}".format(e))
            return False
            
        return True
          
    def load(self, fileName = CONFIGURE_FILE_NAME):
        if not os.path.exists(fileName):
            self._createEmptyConfig()
            return self._save(fileName)
        
        try:        
            with open(fileName, 'r') as json_file:
                self.config = json.load(json_file)
        except Exception as e:
            printError("{}".format(e))
            return False
            
        return True
    
    def _printEditMenu(self):
        print("1) Edit 7z path")
        print("2) Edit perl path")
        print("3) Edit nmake path")
        print("4) Edit vcvarsall path")
        print("5) Edit architecture")
        print("6) Edit version")
        print("7) Return")

    def _runConfigEdit(self):
        while True:
            self._printEditMenu()
            option = ConfigureEditOption(getIntegerInputValue("> "))
            if option == ConfigureEditOption.EDIT_PATH_7z:
                value = getInputValue("Path to directory with 7z.exe: ")
                self.config['paths']['7z'] = value
            elif option == ConfigureEditOption.EDIT_PATH_PERL:
                value = getInputValue("Path to directory with perl.exe: ")
                self.config['paths']['perl'] = value
            elif option == ConfigureEditOption.EDIT_PATH_NMAKE:
                value = getInputValue("Path to directory with nmake.exe: ")
                self.config['paths']['nmake'] = value
            elif option == ConfigureEditOption.EDIT_VCVARSALL:
                value = getInputValue("Path to vcvarsall: ")
                self.config['vcvarsall'] = value
            elif option == ConfigureEditOption.EDIT_ARCHITECTURE:
                value = getInputValue("Architecture (x86 or x64): ")
                self.config['architecture'] = value
            elif option == ConfigureEditOption.EDIT_VERSION:
                value = getInputValue("Version (90 or 140 or 150): ")
                self.config['version'] = value
            elif option == ConfigureEditOption.RETURN:
                self._save()
                break
            else:
                print("Wrong option\n")
        
    def _printMenu(self):
        print("1) Edit config")
        print("2) Check config")
        print("3) Print config")
        print("4) Return")
    
    def run(self):
        while True:
            self._printMenu()
            option = ConfigureOption(getIntegerInputValue("> "))
            if option == ConfigureOption.EDIT_CONFIG:
                self._runConfigEdit()
            elif option == ConfigureOption.CHECK_CONFIG:
                print(self.checkConfig())
            elif option == ConfigureOption.PRINT_CONFIG:
                print(self.printConfig())
            elif option == ConfigureOption.RETURN:
                break
            else:
                print("Wrong option\n")


######################################################################
##BuildEngine#########################################################
######################################################################

class BuildEngine:
    def __init__(self):
        self.compile_flags = "-no-asm"
        self.working_dir = os.getcwd()
        
    def run(self, config, openSSLFile):
        dirname = openSSLFile.replace(".tar.gz","")
        openssl_tar_file = openSSLFile.replace(".gz","")
        
        if config['architecture'] == "x86":
            src_suffix = "_" + "vs" + config['version'] + "_32"
            openssl_flag = "VC-WIN32"
        else:
            src_suffix = "_" + "vs" + config['version'] + "_64"
            openssl_flag = "VC-WIN64A"
            
        vs_tools_env_var = "VS" + config['version'] + "COMNTOOLS"
        dirname_src = dirname + src_suffix
        dirname_bin = dirname + src_suffix + "_build"

        subprocess.call("7z x -y " + openSSLFile) #extract the .gz file
        
        if config['architecture'] == "x86":
            #delete previous directories
            shutil.rmtree(os.getcwd()+'/'+dirname, ignore_errors=True)
            shutil.rmtree(os.getcwd()+'/'+dirname_src, ignore_errors=True)

        #extract tar file for 32
            subprocess.call("7z x -y " + openssl_tar_file)
            os.rename(dirname, dirname_src)

        #Compile 32
            os.chdir(dirname_src)

            print("perl Configure " + openssl_flag + " --prefix=" + os.path.join(self.working_dir,dirname_bin) + " " + self.compile_flags)
            subprocess.call("perl Configure " + openssl_flag + " --prefix=" + os.path.join(self.working_dir,dirname_bin) + " " + self.compile_flags,shell=True)

            if( os.path.exists("ms/do_ms.bat") ):
                subprocess.call("ms\do_ms.bat",shell=True)
                subprocess.call("nmake -f ms/ntdll.mak",shell=True)
                subprocess.call("nmake -f ms/ntdll.mak install",shell=True)
            else:
                subprocess.call("nmake",shell=True)
                subprocess.call("nmake test",shell=True)
                subprocess.call("nmake install",shell=True)

######################################################################
##Main################################################################
######################################################################
        
class MenuOption(Enum):
    CONFIG = 1
    BUILD = 2
    EXIT = 3

def printMenu():
    print("1) Config data")
    print("2) Build")
    print("3) Exit")

if __name__ == "__main__":
    configureEngine = ConfigureEngine()
    buildEngine = BuildEngine()
    
    if not configureEngine.load():
        print("Config error")
        exit(-1)
    
    while True:
        printMenu()
        option = MenuOption(getIntegerInputValue("> "))
            
        if option == MenuOption.CONFIG:
            configureEngine.run()
        elif option == MenuOption.BUILD:
            configStatus = configureEngine.checkConfig()
            if not configStatus:
                print(configStatus)
                continue
            buildEngine.run(configureEngine.config, OPENSSL_FILE_NAME)
            print(configStatus and True)
        elif option == MenuOption.EXIT:
            exit(0)
        else:
            print("Wrong option\n")
            continue
       
    
exit(0)

# args
parser = argparse.ArgumentParser()
parser.add_argument("-f", "--filename", help="First argument must be the tar.gz file of OpenSSL source", required=True)
parser.add_argument("-a", "--arch", help="Second argument must be x86 or amd64", required=True)
parser.add_argument("-v", "--vs_version", help="Visual Studio version (eg:90, 140, 150)", required=True)
parser.set_defaults(writeVersionInfos=False)
args = parser.parse_args()

compile_flags = "-no-asm"
#compile_flags = "-no-asm -no-shared"


openssl_32_flag = "VC-WIN32"
openssl_64_flag = "VC-WIN64A"

working_dir = os.getcwd()

dirname  = args.filename.replace(".tar.gz","")

src_32_suffix = "_" + "vs" + args.vs_version + "_32"
src_64_suffix = "_" + "vs" + args.vs_version + "_64"

vs_tools_env_var = "VS" + args.vs_version + "COMNTOOLS"


if args.arch != "x86" and args.arch != "amd64":
    print("Second argument must be x86 or amd64")
    exit(1)

subprocess.call("7z x -y " + args.filename) #extract the .gz file

dirname_src_32 = dirname + src_32_suffix
dirname_src_64 = dirname + src_64_suffix
dirname_bin_32 = dirname + src_32_suffix + "_build"
dirname_bin_64 = dirname + src_64_suffix + "_build"

openssl_tar_file = args.filename[0:-3]

if args.arch == "x86":

#delete previous directories
    shutil.rmtree(os.getcwd()+'/'+dirname, ignore_errors=True)
    shutil.rmtree(os.getcwd()+'/'+dirname_src_32, ignore_errors=True)

#extract tar file for 32

    subprocess.call("7z x -y " + openssl_tar_file)
    os.rename(dirname, dirname_src_32)

#Compile 32
    os.chdir(dirname_src_32)

    print("perl Configure " + openssl_32_flag + " --prefix=" + os.path.join(working_dir,dirname_bin_32) + " " + compile_flags)
    subprocess.call("perl Configure " + openssl_32_flag + " --prefix=" + os.path.join(working_dir,dirname_bin_32) + " " + compile_flags,shell=True)

    if( os.path.exists("ms/do_ms.bat") ):
        subprocess.call("ms\do_ms.bat",shell=True)
        print(os.getcwd())
        subprocess.call("nmake -f ms/ntdll.mak",shell=True)
        subprocess.call("nmake -f ms/ntdll.mak install",shell=True)
    else:
        subprocess.call("nmake",shell=True)
        subprocess.call("nmake test",shell=True)
        subprocess.call("nmake install",shell=True)

    print("32-bit compilation complete.")

#Go back to base dir
os.chdir(working_dir)
################
#subprocess.call(['espeak', text], stderr=subprocess.DEVNULL)

if args.arch == "amd64":

#delete previous directories
    shutil.rmtree(os.getcwd()+'/'+dirname, ignore_errors=True)
    shutil.rmtree(os.getcwd()+'/'+dirname_src_64, ignore_errors=True)


#extract for 64
    subprocess.call("7z x -y " + openssl_tar_file)
    os.rename(dirname, dirname_src_64)

#Compile 64
    os.chdir(dirname_src_64)

    subprocess.call("perl Configure " + openssl_64_flag + " --prefix=" + os.path.join(working_dir,dirname_bin_64) + " " + compile_flags,shell=True)
    if( os.path.exists("ms\do_ms.bat") ):
        subprocess.call("ms\do_win64a.bat",shell=True)
        subprocess.call("nmake -f ms/ntdll.mak",shell=True)
        subprocess.call("nmake -f ms/ntdll.mak install",shell=True)
    else:
        subprocess.call("nmake",shell=True)
        subprocess.call("nmake test",shell=True)
        subprocess.call("nmake install",shell=True)

    print("64-bit compilation complete.")

#Go back to base dir
os.chdir(working_dir)
################

os.remove(openssl_tar_file)