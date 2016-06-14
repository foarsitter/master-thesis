from mpl_toolkits.mplot3d import Axes3D
import matplotlib.pyplot as plt
import numpy as np
import csv


class SimpleAgent:

    maxX = 0
    maxY = 0

    def __init__(self, x, y, z, group, opinion):

        self.x = int(x)
        self.y = int(y)
        self.z = int(z)
        self.group = int(group)
        self.opinion = float(opinion)

        if self.x > self.maxX:
            SimpleAgent.maxX = self.x

        if self.y > self.maxY:
            SimpleAgent.maxY = self.y


file = "maps/diemen.output.v2.csv"

agents = []

with open(file, newline='') as csv_file:
                reader = csv.reader(csv_file, delimiter=';')
                for row in reader:
                    if(row[0] != 'x'):
                        agents.append(SimpleAgent(row[0], row[1], row[2], row[3], 0))



shape = (SimpleAgent.maxX+1, SimpleAgent.maxY+1)


grid = np.zeros(shape, dtype=np.int)

xs = []
ys = []
zs = []
dx = 1
dy = 1
dz = []

for agent in agents:
    if(agent.group == -1):
        grid[agent.x,agent.y] += 1

for x in range(len(grid)):
    for y in range(len(grid[x])):
        if(grid[x][y] > 0):
            xs.append(x)
            ys.append(y)
            zs.append(0)
            dz.append(grid[x][y])
                 

fig = plt.figure()
ax = fig.add_subplot(111, projection='3d')


ax.bar3d(xs, ys, zs, dx, dy, dz,edgecolor = "none", color = 'b')
ax.grid(False)
ax.set_axis_off()


ax.set_xlabel('X')
ax.set_ylabel('Y')
ax.set_zlabel('Z')




plt.show()
matplotlib.pyplot.close("all")
