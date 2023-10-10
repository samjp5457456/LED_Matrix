from tkinter import *

rows=16
columns=16
btns=[[None for i in range(rows)] for j in range(columns)]
tk=Tk()
tk.title("Pixel Board")

def color(btn):
        btn.configure(bg='red')
    
for i in range(rows):
    for j in range(columns):
        btns[i][j]=Button(tk,padx=10,bg='white')
        btns[i][j]['command']=lambda btn=btns[i][j]:color(btn)
        btns[i][j].grid(row=i,column=j)  
        
redbutton = Button(tk, text = 'Red', fg ='white', bg = 'red', command = "" ) 
redbutton.grid(row = 1, column = 20) 
greenbutton = Button(tk, text = 'Green', fg='white', bg = 'green', command = "") 
greenbutton.grid( row = 2, column = 20 ) 
bluebutton = Button(tk, text ='Blue', fg ='white', bg = 'blue', command = "") 
bluebutton.grid(row = 3, column = 20 ) 
blackbutton = Button(tk, text ='Black', fg ='white', bg = 'black' , command = "") 
blackbutton.grid( row = 4, column = 20) 

                   
tk.mainloop()
