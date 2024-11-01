from tkinter import *
from PIL import ImageTk, Image
from pygame import *
import requests
import os
import time
import numpy as np
#note - constants always go on the top
FONT = ('Courier', 25, 'normal')
FONT_2 = ('Courier', 20, 'normal')
RED = '#e7305b'
GREEN = '#9bdeac'
YELLOW = '#f7f5dd'
starttime = time.time()
timerec = []
count = 0
#note - create 3 functions - play_song, stop_song, and resume_song
def play_song():
    global starttime
    global timerec
    song_queue = music_playlist.get(ACTIVE)
    mixer.music.load(song_queue)
    song_status.set('Playing')
    mixer.music.play()
    starttime = time.time()
    timerec = []
    count = 0
    print("play:", starttime)
def stop_song():
    song_status.set('Stop,save')
    mixer.music.pause()
    np.save('songdat.npy',np.array(timerec))
def resume_song():
    song_status.set('Resume')
    mixer.music.unpause()

#tk() - allows you to register and unregister a callback function which will be called from the Tk mainloop
window = Tk()
window.title('Python Music Player Project')
#padx & pady - a distance - designating external padding on each side of the slave widget
#fg - foreground color & bg - background color
window.config(padx = 15, pady = 15, bg = YELLOW)
#label - this widget implements a display box where you can place text or image
title_label = Label(text = 'Azadi Records, Inc. Recent Releases', fg = RED, bg = YELLOW, font = FONT)
title_label.grid(column = 1, row = 0)


#fg - foreground color & bg - background color
#highlightthickness = 0 - remove the light grey border around the canvas widget
#canvas = Canvas(width = 200, height = 225, bg = YELLOW, highlightthickness = 0)
#PhotoImage - to display images in labels, buttons, canvases, and text widgets
#--p_img = ImageTk.PhotoImage(Image.open('vinyl.jpg'))
#c.create_image(x, y, option, ...)
#c.create_image() - this constructor returns the integer ID number of the image object for that canvas
#--canvas.create_image(100, 112, image = p_img)

mixer.init()
song_status = StringVar()
song_status.set('choosing')

#Python Listbox - https://www.tutorialspoint.com/python/tk_listbox.htm
music_playlist = Listbox(window, selectmode = SINGLE, font = FONT_2, width = 50)
music_playlist.grid(columnspan = 5)

os.chdir("C:/Users/UBTAdmin/Desktop/newarm/")
songs = os.listdir()
for s in songs:
    music_playlist.insert(END, s)

#command calls the play_song function
play_song_button = Button(text = 'Play', highlightthickness = 0, command = play_song)
play_song_button.config(font = FONT_2, padx = 5, pady = 5)
play_song_button.grid(column = 1 , row = 5)
#command calls the stop_song function
stop_song_button = Button(text = 'Stop', highlightthickness = 0, command = stop_song)
stop_song_button.config(font = FONT_2, padx = 5, pady = 5)
stop_song_button.grid(column = 1, row = 6)
#command calls the resume_song function
resume_song_button = Button(text = 'Resume', highlightthickness = 0, command = resume_song)
resume_song_button.config(font = FONT_2, padx = 5, pady = 5)
resume_song_button.grid(column = 1, row = 7)
resume_song_button.grid()


def keyh(event):
    global starttime
    global timerec
    global count
    count += 1
    t = time.time() - starttime
    print("kp:", t, "count:",count)
    timerec.append(t)

window.bind("<k>",keyh)

window.mainloop()