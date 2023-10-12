from tkinter import *
from tkinter import messagebox


def createGrid():
    for i in range(rows):
        for j in range(columns):
            btns[i][j] = Button(tk, padx=10, bg="gray")
            btns[i][j]['command'] = lambda btn=btns[i][j]: color(btn)
            btns[i][j].grid(row=i, column=j)


def createColorButtons():
    for i in range(len(colors)):
        if (colors[i] == "White" or colors[i] == "Yellow"):    
            colorButtons[i] = Button(tk, text=colors[i], fg='black', bg=colors[i].lower())
        else:
            colorButtons[i] = Button(tk, text=colors[i], fg='white', bg=colors[i].lower())
            
        colorButtons[i]['command'] = lambda btn=colorButtons[i]: changeColor(btn)
        colorButtons[i].grid(row=i+5, column=20)


def createClearandErase():
    clearButton = Button(tk, text="Clear", fg="white", bg="gray", height=20, width=10)
    clearButton.grid(row=16, column=20)
    clearButton['command'] = lambda btn=clearButton: clear(btn)

    eraseButton = Button(tk, text="Erase", fg="white", bg="gray", height=15, width=10)
    eraseButton.grid(row=18, column=20)
    eraseButton['command'] = lambda btn=eraseButton: erase(btn)


def clear(btn):
    for i in range(rows):
        for j in range(columns):
            btns[i][j]['bg'] = "gray"


def erase(btn):
    print("Erased")


def color(btn):
    if (userColor == ""):
        print("No color selected")
    else:
        btn['bg'] = userColor


def changeColor(btn):
    global userColor
    userColor = btn['bg']
    
    
def on_closing():
    userColor = "gray"
    if messagebox.askokcancel("Quit", "Do you want to quit?"):
        tk.destroy()


rows = 16
columns = 16
btns = [[None for i in range(rows)] for j in range(columns)]
colors = ["White", "Red", "Green", "Blue", "Black", "Cyan", "Yellow"]
colorButtons = [None for i in range(len(colors))]
clearButton = None
eraseButton = None
tk = Tk()
tk.title("Pixel Board")

createGrid()
createColorButtons()
createClearandErase()

tk.protocol("WM_DELETE_WINDOW", on_closing)
tk.mainloop()
