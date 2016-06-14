# -*- coding: utf-8 -*-
from mpl_toolkits.mplot3d import Axes3D
import matplotlib.pyplot as plt
import numpy as np
from itertools import product, combinations
fig = plt.figure()
ax = fig.gca(projection='3d')
ax.set_aspect("equal")

#draw cube
r = [0, 1]
for s, e in combinations(np.array(list(product(r,r,r))), 2):
    if np.sum(np.abs(s-e)) == r[1]-r[0]:
        ax.plot3D(*zip(s,e), color="b")


#draw a vector
from matplotlib.patches import FancyArrowPatch
from mpl_toolkits.mplot3d import proj3d

class Arrow3D(FancyArrowPatch):
    def __init__(self, xs, ys, zs, *args, **kwargs):
        FancyArrowPatch.__init__(self, (0,0), (0,0), *args, **kwargs)
        self._verts3d = xs, ys, zs

    def draw(self, renderer):
        xs3d, ys3d, zs3d = self._verts3d
        xs, ys, zs = proj3d.proj_transform(xs3d, ys3d, zs3d, renderer.M)
        self.set_positions((xs[0],ys[0]),(xs[1],ys[1]))
        FancyArrowPatch.draw(self, renderer)

ax.add_artist(Arrow3D([0,0],[0,0],[0,1], mutation_scale=20, lw=1, arrowstyle="-|>", color="k"))
ax.add_artist(Arrow3D([0,0],[0,1],[0,0], mutation_scale=20, lw=1, arrowstyle="-|>", color="k"))
ax.add_artist(Arrow3D([0,1],[0,0],[0,0], mutation_scale=20, lw=1, arrowstyle="-|>", color="k"))

ax.add_artist(Arrow3D([1,1],[1,1],[0,1], mutation_scale=20, lw=1, arrowstyle="-|>", color="k"))
ax.add_artist(Arrow3D([1,1],[0,1],[1,1], mutation_scale=20, lw=1, arrowstyle="-|>", color="k"))
ax.add_artist(Arrow3D([0,1],[1,1],[1,1], mutation_scale=20, lw=1, arrowstyle="-|>", color="k"))

ax.add_artist(Arrow3D([1,1],[0,0],[0,1], mutation_scale=20, lw=1, arrowstyle="-|>", color="k"))
ax.add_artist(Arrow3D([1,1],[0,1],[0,0], mutation_scale=20, lw=1, arrowstyle="-|>", color="k"))

ax.add_artist(Arrow3D([0,0],[0,1],[1,1], mutation_scale=20, lw=1, arrowstyle="-|>", color="k"))
ax.add_artist(Arrow3D([0,1],[0,0],[1,1], mutation_scale=20, lw=1, arrowstyle="-|>", color="k"))

ax.add_artist(Arrow3D([0,1],[1,1],[0,0], mutation_scale=20, lw=1, arrowstyle="-|>", color="k"))
ax.add_artist(Arrow3D([0,0],[1,1],[0,1], mutation_scale=20, lw=1, arrowstyle="-|>", color="k"))

ax.text(-0.1, -0.1, -0.1,'Heerde', horizontalalignment='center')
ax.text(-0.2, -0.2, 1,'Emmen', horizontalalignment='center')

ax.text(-0.33, 1, 0,'Diemen', horizontalalignment='center')
ax.text(0, 1.15, 1,'Schiedam', horizontalalignment='center')


ax.text(1.25, 0, 1,'Breda', horizontalalignment='center')
ax.text(1, -0.2, 0,'Groningen', horizontalalignment='center')

ax.text(1.3, 1, 0,'Almere', horizontalalignment='center')
ax.text(1, 1.3, 1,'Rotterdam', horizontalalignment='center')


ax.set_xlabel('inwoners')
ax.set_ylabel('nwal')
ax.set_zlabel('segregatie')

ax.grid(False)
ax.xaxis.set_ticks([])
ax.yaxis.set_ticks([])
ax.zaxis.set_ticks([])

#plt.axis('off')
plt.show()
matplotlib.pyplot.close("all")
