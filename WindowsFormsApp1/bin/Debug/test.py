
# -*- coding: utf-8 -*-
def Testhansuk():
    #data = "
    return data


def TestHong(str):
    # if str == ""
    #     data = "mclab.hufs.ac.kr"
    # else str =="google"테스트
    #     data = "http://www.google.com"
    return str
#########################################################################################

from konlpy.tag import Hannanum

mecab = Hannanum()
sen = '인스타그램에서 야식음식에서 간장을 제외하고 후라이드는 해쉬태그'

#print(mecab.pos(sen))
def first(sen):
    sub = dict(sen)

    li = []

    for key, value in sub.items():
        if value == 'N':
            sub.items()
            li.append(key)
        elif value == 'F':
            li.append(key)
    reject(li)

def check_sen(sen):
    sen = mecab.pos(sen)
    first(sen)


def complete(full):
    for i in range(len(full)):
        if i == 0:
            full[i] = full[i] + " "
        else:
            if "-" not in list(full[i]):
                if "#" not in list(full[i]):
                    full[i] = "+" + full[i]
    last = ''.join(full)
    return last

def reject(mean):
    fee = []

    for i in range(len(mean)):
        if mean[i] != "제외":
            fee.append(mean[i])
        else:
            fee.append("-"+mean[i - 1])
            fee.remove(mean[i-1])
    filetype(fee)

def filetype(file):
    fee = []

    for i in range(len(file)):
        if file[i] == "ppt":
            file[i] = "filetpye:ppt "
            fee.append(file[i])
        elif file[i] == "xls":
            file[i] = "filetpye:xls "
            fee.append(file[i])
        elif file[i] == "pdf":
            file[i] = "filetpye:pdf "
            fee.append(file[i])
        elif file[i] == "doc":
            file[i] = "filetpye:doc "
            fee.append(file[i])
        else:
            fee.append(file[i])
    site(fee)

def site(word):
    fee = []
    for i in range(len(word)):
        if word[i] == "인스타그램":
            word[i] = 'site:www.Instagram.com'
            fee.append(word[i])
        elif word[i] == "네이버":
            word[i] = 'site:www.naver.com'
            fee.append(word[i])
        elif word[i] == "페이스북":
            word[i] = 'site:www.facebook.com'
            fee.append(word[i])
        elif word[i] == "구글":
            word[i] = 'site:www.goole.com'
            fee.append(word[i])
        else:
            fee.append(word[i])
    hash(fee)

def hash(hash):
    fee = []
    for i in range(len(hash)):
        if hash[i] != "해쉬태그":
            fee.append(hash[i])
        else:
            fee.append(" #" + hash[i - 1])
            fee.remove(hash[i - 1])
    complete(fee)

check_sen(sen)
print("end")
