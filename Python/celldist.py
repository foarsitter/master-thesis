import csv
import numpy as np
import math
import matplotlib.pyplot as plt
import glob, os, shutil
# we have an .csv file with x,y,z coords an opinion value and a group value.


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

history = np.zeros((50,101), dtype=np.float)
inv_history = np.zeros((101,50), dtype=np.float)

cities = ["diemen","schiedam","emmen","breda","groningen","almere","rotterdam"]

for city in cities:

    base_dir = "C:/RugJelmertModelingOutput/7mei/{0}/{0}/data".format(city)

    os.chdir(base_dir)

    runs = os.listdir()
    moore_squared = 5
            
    for run in range(len(runs)):

        it = 0; 
        
        for file in glob.glob("{0}/grid-dumps/*.grid.csv".format(runs[run])):       

            agents = []

            #read and parse the csv doc.
            with open(file, newline='') as csv_file:
                reader = csv.reader(csv_file, delimiter=';')
                for row in reader:
                    agents.append(SimpleAgent(row[0], row[1], row[2], row[3], row[4].replace(',', '.')))


            shape = (SimpleAgent.maxX+1, SimpleAgent.maxY+1)

            grid_outgroup = np.zeros(shape, dtype=np.float)
            grid_outgroup_count = np.zeros(shape, dtype=np.float)
            grid_ingroup = np.zeros(shape, dtype=np.float)
            grid_ingroup_count = np.zeros(shape, dtype=np.float)

            for agent in agents:

                x = agent.x
                y = agent.y

                if agent.group == -1:
                    grid_outgroup[x,y] += agent.opinion
                    grid_outgroup_count[x,y] += 1
                else:
                    grid_ingroup[x, y] += agent.opinion
                    grid_ingroup_count[x, y] += 1


            ingroup_mean = np.divide(grid_ingroup,grid_ingroup_count)
            outgroup_mean = np.divide(grid_outgroup , grid_outgroup_count)


            cell_abs_shape = (int(shape[0]/moore_squared), int(shape[1]/moore_squared))
            cell_abs = np.zeros(cell_abs_shape, dtype=np.float)

            for i in range(cell_abs_shape[0]):
                for j in range(cell_abs_shape[1]):

                    ingroup_sum = 0
                    outgroup_sum = 0
                    count = 0

                    for m in range(moore_squared):
                        for n in range(moore_squared):

                            x = i * moore_squared + m
                            y = j * moore_squared + n

                            if grid_ingroup_count[x,y] > 0 and grid_outgroup_count[x,y] > 0:
                                count += 1
                                ingroup_sum += ingroup_mean[x, y]
                                outgroup_sum += outgroup_mean[x, y]


                    if count > 0:
                        cell_abs[i,j] = abs((outgroup_sum / count) - (ingroup_sum / count) )
            #create for each group a numpy array with x,y and the avg value of each group.

            #create two numpy array's for each group

            c_count = 0
            c_sum = 0

            for elm in np.nditer(cell_abs):   
                if elm != 0:
                    c_count += 1
                    c_sum += elm

            history[run,it] = c_sum / c_count
            inv_history[it,run] = c_sum / c_count
            it += 1
        plt.plot(history[run]) 

    plt.grid(True)
    plt.xlim(0,101)
    plt.ylim(-0.1,2.1)

    plt.savefig("C:/RugJelmertModelingOutput/7mei/{0}/_aggregated/pdf/cell.dist.{1}.pdf".format(city,moore_squared,'.pdf',), bbox_inches='tight')
    plt.savefig("C:/RugJelmertModelingOutput/7mei/{0}/_aggregated/png/cell.dist.{1}.png".format(city,moore_squared,'.png'), bbox_inches='tight')
    np.savetxt("C:/RugJelmertModelingOutput/7mei/{0}/_aggregated/csv/cell.dist.{1}.aggregated.csv".format(city,moore_squared), history, delimiter=';')

    aggregated = []

    for run in range(len(inv_history)):
        aggregated.append(str(inv_history[run].mean()))

    with open("C:/RugJelmertModelingOutput/7mei/{0}/_aggregated/csv/mean/cell.dist.{1}.avg.csv".format(city,moore_squared), "w") as f:   
        print("\n".join(aggregated), file=f)

    plt.clf()
    plt.grid(True)
    plt.xlim(0,101)
    plt.ylim(-0.1,2.1)
    plt.plot(aggregated)
    plt.savefig("C:/RugJelmertModelingOutput/7mei/{0}/_aggregated/png/mean/cell.dist.{1}.png".format(city,moore_squared,'.png'), bbox_inches='tight')
    print("{} done...".format(city))
print("Done...")
