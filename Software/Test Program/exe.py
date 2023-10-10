from tkinter import *

rows = 16
columns = 16

btns = [[None for i in range(rows)] for j in range(columns)]
master = Tk()
master.title("My First Application!")
master.geometry("500x500")

def changeColor(btn):
    btn.configure(bg="blue")

for i in range(rows):
    for j in range(columns):
        btns[i][j]=Button(master, padx=10, bg="white")
        btns[i][j].grid(row = i, column = j)
        btns[i][j]['command'] = lambda btn=btns[i][j]:changeColor(btn)

master.mainloop()