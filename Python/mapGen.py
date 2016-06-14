import csv as csv
from random import randint
xMax = 100
yMax = 100

Grid = [[0 for x in range(yMax)] for x in range(xMax)] 

outGroupSize = 0;

#open a new file
with open('output.csv', 'w', newline='') as csv_file:
    #open csv writer
    writer = csv.writer(csv_file, delimiter=';')
    
    #write the heading
    writer.writerow(("x", "y","z", "group", "opinion"))        
    
    #populate the grid with actors for the ingroup
    for x in range(xMax):
        for y in range(yMax):
            writer.writerow((x, y,0, "1", "random"))        
            Grid[x][y] += 1;
            
    #populate the grid with actors for the outgroup with a random location
    for o in range(outGroupSize):
        x = randint(0,xMax-1)
        y = randint(0,yMax-1)
        writer.writerow((x, y,Grid[x][y], "-1", "random"))        
        Grid[x][y]+=1
