def get_output_path(input_path):
    input_path = input_path.split('/')
    output_path = ""
    for i in range(len(input_path)-1):
        output_path += input_path[i] + '/'
    output_path += "output.txt"
    return output_path

def get_Dikarev_path(input_path):
    input_path = input_path.split('/')
    output_path = ""
    for i in range(len(input_path)-1):
        output_path += '/' + input_path[i]
    output_path += '/' + input_path[len(input_path)-1] + "_Dikarev.txt"
    return output_path

def get_file_list(file_path):
    file = open(file_path)
    input_list = file.read()
    input_list = input_list.split('\n')
    return input_list



class PolTuring():
    def __init__(self):
        return None

    #Зводить усі команди до першого стану
    def to_first_states(self, input_path, first_state):
        output_path = get_output_path(input_path)
        output_file = open(output_path, "w")

        input_list = get_file_list(input_path)

        for line in input_list:
            line_list = line.split('\t') 
            output_file.write(line_list[0])
            end = True if (line_list[0] == ' ') else False

            for i in range(1, len(line_list)):
                output_file.write('\t')
                if line_list[i] != '':
                    
                    if len(line_list[i]) == 3:
                        if line_list[i][2] == '0':
                            output_file.write(line_list[i][0] + line_list[i][1] + str(0))
                            continue
                        state = int(line_list[i][2]) 
                    elif len(line_list[i]) == 4:
                        state = int(line_list[i][2]+line_list[i][3]) 
                    else:
                        state = int(line_list[i][2]+line_list[i][3]+line_list[i][4])  

                    output_file.write(line_list[i][0] + line_list[i][1] + str(int(state)-first_state+1))
            
            if (end):
                return
            output_file.write('\n')
   

    #Склеїти файли
    def merge(self, file1, file2, file1_last_state):
        output_path = get_output_path(file1)
        output_file = open(output_path, "w")

        input_list1 = get_file_list(file1)
        input_list2 = get_file_list(file2)

        file1_dict = {}
        file2_dict = {}

        for line in input_list1:
            if len(line) == 0:
                continue
            file1_dict[line[0]] = line[1:len(line)-1]

        for line in input_list2:
            if len(line) == 0:
                continue
            file2_dict[line[0]] = line[1:len(line)-1]
        

        for i in file1_dict.keys():
            if i == ' ':
                continue
            output_file.write(i)
            output_file.write(file1_dict[i])
            if i in file2_dict.keys():
                line_list = file2_dict[i].split('\t')
                for i in range(len(line_list)):
                    output_file.write('\t')
                    if line_list[i] != '':
                        if len(line_list[i]) == 2:
                            output_file.write(line_list[i][0] + line_list[i][1] + str(0))
                            continue
                        elif len(line_list[i]) == 3:
                            if line_list[i][2] == '0':
                                output_file.write(line_list[i][0] + line_list[i][1] + str(0))
                                continue
                            state = int(line_list[i][2]) 
                        elif len(line_list[i]) == 4:
                            state = int(line_list[i][2]+line_list[i][3]) 
                        else:
                            state = int(line_list[i][2]+line_list[i][3]+line_list[i][4])  
                        
                        output_file.write(line_list[i][0] + line_list[i][1] + str(int(state)+file1_last_state))
            output_file.write('\n')


        for i in file2_dict.keys():
            if (i in file1_dict.keys()):
                continue
            else:
                output_file.write(i)
                output_file.write('\t'*(file1_last_state-1))
                line_list = file2_dict[i].split('\t')
                for i in range(len(line_list)):
                    output_file.write('\t')
                    if line_list[i] != '':
                        if len(line_list[i]) == 2:
                            output_file.write(line_list[i][0] + line_list[i][1] + str(0))
                            continue
                        elif len(line_list[i]) == 3:
                            if line_list[i][2] == '0':
                                output_file.write(line_list[i][0] + line_list[i][1] + str(0))
                                continue
                            state = int(line_list[i][2]) 
                        elif len(line_list[i]) == 4:
                            state = int(line_list[i][2]+line_list[i][3]) 
                        else:
                            state = int(line_list[i][2]+line_list[i][3]+line_list[i][4])  
                        
                        output_file.write(line_list[i][0] + line_list[i][1] + str(int(state)+file1_last_state))
                output_file.write('\n')

        
        output_file.write(' ')
        output_file.write(file1_dict[' '])
        line_list = file2_dict[' '].split('\t')
        for i in range(len(line_list)):
            output_file.write('\t')
            if line_list[i] != '':
                if len(line_list[i]) == 2:
                    output_file.write(line_list[i][0] + line_list[i][1] + str(0))
                    continue
                elif len(line_list[i]) == 3:
                    if line_list[i][2] == '0':
                        output_file.write(line_list[i][0] + line_list[i][1] + str(0))
                        continue
                    state = int(line_list[i][2]) 
                elif len(line_list[i]) == 4:
                    state = int(line_list[i][2]+line_list[i][3]) 
                else:
                    state = int(line_list[i][2]+line_list[i][3]+line_list[i][4])  
                output_file.write(line_list[i][0] + line_list[i][1] + str(int(state)+file1_last_state))
                
        return


##Start##
file1_path = "C://Users//Rog2//Desktop//Turing machine tools//Third.txt"
file2_path = "C://Users//Rog2//Desktop//Turing machine tools//x^y.txt"
file1_last_state = 187

Turing = PolTuring()
Turing.merge(file1_path, file2_path, file1_last_state)