import tkinter
master = tkinter.Tk()
master.title("My First Application!")
master.geometry("750x750")

for x in range(1, 17):
    for y in range(1, 17):
        b = tkinter.Button(master, text="button", command=master.destroy,
                           height=2, width=2, bg="black",fg="white")
        b.pack()
        b.grid(row=x, column=y)

master.mainloop()