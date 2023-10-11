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
tk = Tk()
tk.title("Pixel Board")

createGrid()
createColorButtons()

tk.protocol("WM_DELETE_WINDOW", on_closing)
tk.mainloop()
