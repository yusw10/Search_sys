from konlpy.tag import Hannanum

class start:
    li = []
    def __init__(self, sen):
        self.name = sen

    def first(self):
        mecab = Hannanum()
        self.name = mecab.pos(self.name)
        sen = self.name
        sub = dict(sen)
        print(sub)

        for key, value in sub.items():
            if value == 'N':
                val = start.reject(self,key)
                self.li.append(val)
            elif value == 'F':
                self.li.append(key)
            elif value == 'M':
                if key == "혹은" or key == "또는":
                    self.li[-1] = self.li[-1] + " OR"
            elif value == 'S':
                if key == "~":
                    self.li[-1] = self.li[-1] + " .."

        print(self.li)

        start.filetype(self.li)

    def reject(self, key):
        if key != "제외":
            fee = key

        else:
            temp = self.li[-1]
            del self.li[-1]
            temp = " -" + temp
            fee = temp
        return fee

    def filetype(file):
        fee = []

        for i in range(len(file)):
            if file[i] == "ppt":
                file[i] = "filetype:ppt"
                fee.append(file[i])
            elif file[i] == "xls":
                file[i] = "filetype:xls"
                fee.append(file[i])
            elif file[i] == "pdf":
                file[i] = "filetype:pdf"
                fee.append(file[i])
            elif file[i] == "doc":
                file[i] = "filetype:doc"
                fee.append(file[i])
            else:
                fee.append(file[i])
        start.site(fee)

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
            elif word[i] == "유투브":
                word[i] = 'site:www.youtube.com'
                fee.append(word[i])
            else:
                fee.append(word[i])
        start.hash(fee)

    def hash(hash):
        fee = []
        for i in range(len(hash)):
            if hash[i] != "해쉬태그":
                if 'OR' in hash[i-1]:
                    two = hash[i-1] + " " + hash[i]
                    fee.remove(hash[i-1])
                    fee.append(two)
                else:
                    fee.append(hash[i])

            else:
                temp = fee[-1]
                del fee[-1]
                fee.append(" #" + temp)

        finish.complete(fee)

class finish(start):

    def complete(full):
        list = []
        for i in range(len(full)):
            if i == 0:
                full[i] = full[i] + " "
                list.append(full[i])
            else:
                if "-" not in full[i]:
                    if "#" not in full[i]:
                        if '..' in full[i-1]:
                            full[i] = full[i] + full[i+1]
                            list.append(full[i])
                        else:
                            full[i] = " +" + full[i]
                            list.append(full[i])

        print(full)
        return print(''.join(full))

my = start("")
my.first()