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
    EDIT_PATH_VCVARSALL = 4
    EDIT_PATH_OPENSSL_FILE = 5
    RETURN = 6

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
        
    def _analyseVcvarsallStatus(self):
        if not os.path.exists(self.config['openSSLFilePath']):
            printError("openSSLFile not found")
            return False
            
        return True
            
    def _analyseOpenSSLFilePathStatus(self):
        if not os.path.exists(self.config['openSSLFilePath']):
            printError("openSSLFile not found")
            return False
            
        return True
    
    def getVcvarsallPath(self):
        return self.config['vcvarsall']
        
    def getOpenSSLFilePath(self):
        return self.config['openSSLFilePath']
 
    def _checkIfCmdExists(self, cmd):
        if(subprocess.call("where " + cmd, shell=True, stdout=subprocess.PIPE, stderr=subprocess.PIPE) == 0):
            return True
        else:
            return False
    
    def checkConfig(self):
        return self._analysePathsStatus(True) and self._analyseVcvarsallStatus() and self._analyseOpenSSLFilePathStatus()
        
    def printConfig(self):
        print(self.config)
    
    def _createEmptyConfig(self):
        self.config.clear()
        self.config['paths'] = { '7z': "", 'perl': "", 'nmake': ""}
        self.config['vcvarsall'] = ""
        self.config['openSSLFilePath'] = ""
        
    def _save(self, fileName = CONFIGURE_FILE_NAME):
        printDebug("Saving config: {}".format(fileName))
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
        print("5) Edit openSSLFilePath path")
        print("6) Return")

    def _runConfigEdit(self):
        while True:
            self._printEditMenu()
            try:
                option = ConfigureEditOption(getIntegerInputValue("> "))
            except Exception as e:
                printError("{}".format(e))
                print("Wrong option\n")
                continue
            
            if option == ConfigureEditOption.EDIT_PATH_7z:
                value = getInputValue("Path to directory with 7z.exe: ")
                self.config['paths']['7z'] = value
            elif option == ConfigureEditOption.EDIT_PATH_PERL:
                value = getInputValue("Path to directory with perl.exe: ")
                self.config['paths']['perl'] = value
            elif option == ConfigureEditOption.EDIT_PATH_NMAKE:
                value = getInputValue("Path to directory with nmake.exe: ")
                self.config['paths']['nmake'] = value
            elif option == ConfigureEditOption.EDIT_PATH_VCVARSALL:
                value = getInputValue("Path to vcvarsall.bat: ")
                self.config['vcvarsall'] = value
            elif option == ConfigureEditOption.EDIT_PATH_OPENSSL_FILE:
                value = getInputValue("Path to openSSLFile: ")
                self.config['openSSLFilePath'] = value
            elif option == ConfigureEditOption.RETURN:
                self._save()
                break
        
    def _printMenu(self):
        print("1) Edit config")
        print("2) Check config")
        print("3) Print config")
        print("4) Return")
    
    def run(self):
        while True:
            self._printMenu()
            try:
                option = ConfigureOption(getIntegerInputValue("> "))
            except Exception as e:
                printError("{}".format(e))
                print("Wrong option\n")
                continue
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
##Main################################################################
######################################################################
        
class MenuOption(Enum):
    CONFIG = 1
    BUILD_X64 = 2
    BUILD_X86 = 3
    EXIT = 4

def printMenu():
    print("1) Config data")
    print("2) Build x64")
    print("3) Build x86")
    print("4) Exit")

if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("-d", "--debug", help="Enable debug mode", required=False, type=ast.literal_eval, default=True)
    args = parser.parse_args()
    if args.debug == True:
        DEBUG_INFO = True
    else:
        DEBUG_INFO = False
    
    configureEngine = ConfigureEngine()
    
    if not configureEngine.load():
        print("Config error")
        exit(-1)
    
    while True:
        printMenu()
        try:
            option = MenuOption(getIntegerInputValue("> "))
        except Exception as e:
            printError("{}".format(e))
            print("Wrong option\n")
            continue
            
        if option == MenuOption.CONFIG:
            configureEngine.run()
        elif option == MenuOption.BUILD_X64 or option == MenuOption.BUILD_X86:
            configStatus = configureEngine.checkConfig()
            if not configStatus:
                print(configStatus)
                continue
            if option == MenuOption.BUILD_X64:
                subprocess.call('{}\\targetOpenSSL_x64.cmd "{}" "{}" {}'.format(os.getcwd(),configureEngine.getVcvarsallPath(), configureEngine.getOpenSSLFilePath(),args.debug), shell=False)
            else:
                print("{}\\CompileOpenSSL_x86.bat {} {}".format(os.getcwd(),configureEngine.getVcvarsallPath(), args.debug))
                subprocess.call('{}\\targetOpenSSL_x86.cmd "{}" "{}" {}'.format(os.getcwd(),configureEngine.getVcvarsallPath(), configureEngine.getOpenSSLFilePath(),args.debug), shell=False)
            print(configStatus)
        elif option == MenuOption.EXIT:
            break