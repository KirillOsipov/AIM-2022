import csv
import math
import pylab as pl
from sklearn.model_selection import train_test_split
from sklearn.neighbors import KNeighborsClassifier
from sklearn.preprocessing import StandardScaler
from matplotlib.colors import ListedColormap

header=["Продукт", "Сладость", "Хруст", "Класс"]
food=["Яблоко", "Салат", "Бекон", "Банан", "Орехи", "Рыба", "Сыр", "Виноград", "Морковь", "Апельсин"]
sweetness=[7,2,1,9,1,1,1,8,2,6]
crunch=[7,5,2,1,5,1,1,1,8,1]
classes=["Фрукт","Овощ","Протеин","Фрукт","Протеин","Протеин","Протеин","Фрукт","Овощ","Фрукт"]
dataRow=[10]

with open("data.csv", mode="w") as w_file:
    writer=csv.writer(w_file, delimiter=";", lineterminator="\n")
    writer.writerow(header)
    for i in range(0,10):
        dataRow=[food[i], sweetness[i], crunch[i], classes[i]]
        writer.writerow(dataRow)

with open("data.csv", mode="r") as r_file:
    dataset=[]
    reader=csv.reader(r_file, delimiter=";",lineterminator="\n")

    for row in reader:
        if row[3] == 'Фрукт':
            dataset.append([row[0], int(row[1]), int(row[2]), 0])
        if row[3] == 'Овощ':
            dataset.append([row[0], int(row[1]), int(row[2]), 1])
        if row[3] == 'Протеин':
            dataset.append([row[0], int(row[1]), int(row[2]), 2])


def build_graphic(data):
    colormap  = ListedColormap(['#FF0000', '#00FF00','#FFA500', '#0000FF'])
    pl.scatter([data[i][1] for i in range(len(data))],
               [data[i][2] for i in range(len(data))],
               c=[data[i][3] for i in range(len(data))],
               cmap=colormap)
    pl.show()


def knn_classify(trainData, testData, k, numberOfClasses):
    def dist(a,b):
        return math.sqrt((a[0]-b[0])**2+(a[1]-b[1])**2)
    predictedCLasses=[]
    for testProduct in testData:
        testDistance=[]
        for i in range(len(trainData)):
            distance=[dist([testProduct[1],testProduct[2]], [trainData[i][1],trainData[i][2]]), trainData[i][3]]
            testDistance.append(distance)
        stat=[0 for _ in range(numberOfClasses)]
        for d in sorted(testDistance)[0:k]:
            stat[d[1]]+=1
        print("Продукт",testProduct[0], "имеет сладость =", testProduct[1]
        ,"и хруст =",testProduct[2],"относится к типам Фрукт, Овощ, Протеин, Злаки:", stat)
        predictedCLasses.append(sorted(zip(stat, range(numberOfClasses)),reverse=True)[0][1])
    return predictedCLasses


def knn_sklearn(points, y, k):
    point_train, point_test, class_train,\
    class_test = train_test_split(points,
    y,test_size=0.2, shuffle=False, stratify=None)

    scaler=StandardScaler()
    scaler.fit(point_train)

    point_train=scaler.transform(point_train)
    point_test=scaler.transform(point_test)

    model = KNeighborsClassifier(n_neighbors=k)
    model.fit(point_train, class_train)
    predict=model.predict(point_test)
    return point_train, point_test, class_train, class_test, predict


barrier=6
testData= dataset[barrier + 1:]
predicts=knn_classify(dataset[:barrier], testData, 2, 3)


print("Метод k-ближайших соседей")
print("Тестовые данные (Фрукт=0, Овощ=1, Протеин=2)", predicts)
for i in range(0, len(predicts)):
    if predicts[i]==0:
        type= 'Фрукт'
    if predicts[i]==1:
        type= 'Овощ'
    if predicts[i]==2:
        type= 'Протеин'
    print("Продукт", testData[i][0],
    "относится к типу", type)


print("Библиотека sklearn")
points=[]
classes=[]
for i in range(len(dataset)):
    points.append([dataset[i][1], dataset[i][2]])
    classes.append(dataset[i][3])
point_train, point_test, classes_train, classes_test, predict=knn_sklearn(points, classes, 2)
print("Те же продукты относятся к типам (Фрукт=0, Овощ=1, Протеин=2): ", predict)
build_graphic(dataset)



header=["Продукт", "Сладость", "Хруст", "Класс"]
food=["Яблоко", "Салат", "Бекон", "Банан", "Орехи", "Рыба", "Сыр", "Виноград", "Морковь",
"Апельсин", "Ячмень", "Авокадо", "Кукуруза", "Овсянка", "Томат", "Киви", "Пшено", "Персик", "Редис", "Перловка"]
sweetness=[7,2,1,9,1,1,1,8,2,6, 1,2,5,8,6,7,2,9,1,3]
crunch=[7,5,2,1,5,1,1,1,8,1, 5,7,3,10,2,5,3,4,6,2]
classes=["Фрукт", "Овощ", "Протеин", "Фрукт", "Протеин", "Протеин", "Протеин", "Фрукт", "Овощ", "Фрукт",
         "Злаки", "Фрукт", "Злаки", "Злаки", "Овощ", "Фрукт", "Злаки", "Фрукт", "Овощ", "Злаки"]
dataRow=[20]

with open("new_data.csv", mode="w") as w_file:
    writer=csv.writer(w_file, delimiter=";", lineterminator="\n")
    writer.writerow(header)
    for i in range(0,19):
        dataRow=[food[i], sweetness[i], crunch[i], classes[i]]
        writer.writerow(dataRow)

with open("new_data.csv", mode="r") as r_file:
    dataset=[]
    reader=csv.reader(r_file, delimiter=";",lineterminator="\n")
    count=0
    for row in reader:
        if count != 0:
            if row[3]=='Фрукт':
                type=0
            if row[3]=='Овощ':
                type=1
            if row[3]=='Протеин':
                type=2
            if row[3]=='Злаки':
                type=3
            dataset.append([row[0], int(row[1]), int(row[2]), type])
        else: count+=1



barrier=14
testData= dataset[barrier + 1:]

predicts=knn_classify(dataset[:14], testData, 3, 4)
print("Метод k-ближайших соседей с дополнительным классом")
print("Тестовые данные (Фрукт=0, Овощ=1, Протеин=2, Злаки=3)", predicts)
for i in range(0, len(predicts)):
    if predicts[i]==0:
        type= 'Фрукт'
    if predicts[i]==1:
        type= 'Овощ'
    if predicts[i]==2:
        type= 'Протеин'
    if predicts[i]==3:
        type= 'Злаки'
    print("Продукт", testData[i][0],
    "принадлежит к типу", type)


print("Библиотека sklearn")
points=[]
classes=[]
for i in range(len(dataset)):
    points.append([dataset[i][1], dataset[i][2]])
    classes.append(dataset[i][3])
point_train, point_test, classes_train, classes_test, predict=knn_sklearn(points, classes, 4)
print("Те же продукты относятся к типам (Фрукт=0, Овощ=1, Протеин=2, Злаки=3):", predict)
build_graphic(dataset)