from tkinter import *


def createGrid():
    for i in range(rows):
        for j in range(columns):
            btns[i][j] = Button(tk, padx=10, bg='white')
            btns[i][j]['command'] = lambda btn=btns[i][j]: color(btn)
            btns[i][j].grid(row=i, column=j)


def createColorButtons():
    for i in range(len(colors)):
        colorButtons[i] = Button(tk, text=colors[i], fg='white', bg=colors[i].lower())
        colorButtons[i]['command'] = lambda btn=colorButtons[i]: changeColor(btn)
        colorButtons[i].grid(row=i+5, column=20)


def color(btn):
    btn.configure(bg=userColor)


def changeColor(btn):
    userColor = btn['text'].lower()


rows = 16
columns = 16
btns = [[None for i in range(rows)] for j in range(columns)]
colors = ["White", "Red", "Green", "Blue", "Black"]
colorButtons = [None for i in range(len(colors))]
tk = Tk()
tk.title("Pixel Board")
userColor = "SystemButtonFace"

createGrid()
createColorButtons()
tk.mainloop()
