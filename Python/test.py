from numpy import genfromtxt
import numpy as np
import matplotlib.pyplot as plt
import glob, os, shutil

my_data = genfromtxt("C:\\RugJelmertModelingOutput\\RugJelmertModelingOutput\\rotterdam\\_aggregated\\absmean.aggregated.csv", delimiter=';')
avg = my_data[0];

for i in range(1,len(my_data)):
    avg += my_data[i]
    plt.plot(my_data[i])

plt.grid(True)
plt.xlim(0,100)

ylim = plt.ylim()

#print(ylim[0])

#plt.ylim(-0.1,1.1)

if ylim[0] < 0:
    plt.ylim(-1.1,1.1)
else:
    plt.ylim(-0.1,1.1)


plt.show()

sys.exit() 
