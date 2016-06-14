from numpy import genfromtxt
import numpy as np
import matplotlib.pyplot as plt
import glob, os, shutil
import csv

#

base_path = "C:/RugJelmertModelingOutput/7mei";

cities = ["heerde","diemen","schiedam","emmen","breda","groningen","almere","rotterdam"]


if(os.path.isdir("{0}/{1}".format(base_path,"summary"))):
    shutil.rmtree("{0}/{1}".format(base_path,"summary"))

os.mkdir("{0}/{1}".format(base_path,"summary"))

# we have in each city /csv/mean/*.avg.csv files who need to be merged.

files = ['absmean.avg.csv', 'absmean_immigrants.avg.csv',"cellavgdistance.avg.csv","cell.dist.5.avg.csv",'absmean_locals.avg.csv', 'absvariance.avg.csv', 'absvariance_immigrants.avg.csv', 'absvariance_locals.avg.csv', 'diversity.avg.csv', 'diversity_immigrants.avg.csv', 'diversity_locals.avg.csv', 'extreme.avg.csv', 'extreme_immigrants.avg.csv', 'extreme_locals.avg.csv', 'mean.avg.csv', 'mean_immigrants.avg.csv', 'mean_locals.avg.csv', 'polarisation.avg.csv', 'polarisation_immigrants.avg.csv', 'polarisation_locals.avg.csv', 'rpbi.avg.csv', 'rpbi_abs.avg.csv', 'variance.avg.csv', 'variance_immigrants.avg.csv', 'variance_locals.avg.csv']

print(files)
print(cities)
print("Create summary for {0} measures.".format(len(files)))

        

for file in files:
    
    print("Summary for {0}".format(file))
    

    with open("{0}/summary/{1}".format(base_path,file), 'w', newline='') as csv_file:    
        writer = csv.writer(csv_file, delimiter=';')

        for city in cities:
            print("City {0} file {1}".format(city,file))

            with open("{0}/{1}/_aggregated/csv/mean/{2}".format(base_path,city,file)) as read_file:
                contents = read_file.read()                
                writer.writerow([city] + contents.split("\n"))
  
            


