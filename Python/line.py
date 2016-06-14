from numpy import genfromtxt
import numpy as np
import matplotlib.pyplot as plt
import glob, os, shutil

#

os.chdir("C:\\RugJelmertModelingOutput\\test\\")

cities = ["1x10_ingroup.half","1x10_ingroup"]
#cities = ["heerde","diemen","schiedam","emmen","breda","groningen","almere","rotterdam"]
#cities = ["01_Heerde","02_Diemen","03_Schiedam","04_Emmen","05_Breda","06_Groningen","07_Almere","08_Rotterdam"]

for city in cities:

    os.chdir("C:\\RugJelmertModelingOutput\\test\\")
    os.chdir(city)
    os.chdir("_aggregated");

    print(os.getcwd())
    
    if(os.path.isdir("png")):
        shutil.rmtree("png")

    if(os.path.isdir("pdf")):
        shutil.rmtree("pdf")

    if(os.path.isdir("csv")):
        shutil.rmtree("csv")

    os.mkdir("png")
    os.mkdir("png/mean")
    os.mkdir("pdf")
    os.mkdir("csv")
    os.mkdir("csv/mean")

    for file in glob.glob("*.aggregated.csv"):
        my_data = genfromtxt(file, delimiter=';')

        avg = my_data[0];
        
        for i in range(0,len(my_data)):

            if i > 0:
                avg += my_data[i]
            plt.plot(my_data[i])
        
        avg /= len(my_data)

        split = file.split('.')

        plt.grid(True)
        plt.xlim(0,100)

        ylim = plt.ylim()

        if ylim[0] < 0:
            plt.ylim(-1.1,1.1)
        elif ylim[1] > 1.1:
            plt.ylim(0,2.1)
        else:
            plt.ylim(-0.1,1.1)
        
        #plt.title(split[0])
        plt.savefig("pdf/{0}{1}".format(split[0],'.pdf'), bbox_inches='tight')
        plt.savefig("png/{0}{1}".format(split[0],'.png'), bbox_inches='tight')
        plt.clf()

        plt.plot(avg)
        #plt.title("".join([split[0]," AVG"]))
        
        plt.grid(True)
        plt.xlim(0,100)

        ylim = plt.ylim()

        if ylim[0] < 0:
            plt.ylim(-1.1,1.1)
        elif ylim[1] > 1.1:
            plt.ylim(0,2.1)
        else:
            plt.ylim(-0.1,1.1)


        np.savetxt("csv/mean/{0}{1}".format(split[0],'.avg.csv'), avg, delimiter=";")
        plt.savefig("png/mean/{0}{1}".format(split[0],'.avg.png'), bbox_inches='tight')
        plt.clf()

        shutil.copy(file, "".join(["csv/",file]))


    print("Done..")
