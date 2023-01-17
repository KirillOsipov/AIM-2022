import pandas
import matplotlib.pyplot as pyplot
import pylab
from sklearn.model_selection import train_test_split
from sklearn.tree import DecisionTreeClassifier
from sklearn.neighbors import KNeighborsClassifier
from sklearn.discriminant_analysis import LinearDiscriminantAnalysis

cars_dataset = pandas.read_csv('car_data.csv')

cars_dataset['Price_Class'] = 'Other'
price_classes = ['Very low', 'Low', 'Middle', 'High', 'Very high']

# Добавление столбца для классификации цен
for i in range(len(cars_dataset)):
    if 0.0 <= cars_dataset['Selling_Price'][i] < 7.0:
        cars_dataset['Price_Class'][i] = price_classes[0]
        continue
    if 7.0 <= cars_dataset['Selling_Price'][i] < 14.0:
        cars_dataset['Price_Class'][i] = price_classes[1]
        continue
    if 14.0 <= cars_dataset['Selling_Price'][i] < 21.0:
        cars_dataset['Price_Class'][i] = price_classes[2]
        continue
    if 21.0 <= cars_dataset['Selling_Price'][i] < 28.0:
        cars_dataset['Price_Class'][i] = price_classes[3]
        continue
    if 28.0 <= cars_dataset['Selling_Price'][i] <= 35.0:
        cars_dataset['Price_Class'][i] = price_classes[4]
        continue


cars_dataset = cars_dataset.drop('Present_Price', axis=1)
cars_dataset = cars_dataset.drop('Selling_Price', axis=1)

cars_dataset['Car_Name'] = pandas.factorize(cars_dataset['Car_Name'])[0]
cars_dataset['Year'] = pandas.factorize(cars_dataset['Year'])[0]
cars_dataset['Kms_Driven'] = pandas.factorize(cars_dataset['Kms_Driven'])[0]
cars_dataset['Fuel_Type'] = pandas.factorize(cars_dataset['Fuel_Type'])[0]
cars_dataset['Seller_Type'] = pandas.factorize(cars_dataset['Seller_Type'])[0]
cars_dataset['Transmission'] = pandas.factorize(cars_dataset['Transmission'])[0]
cars_dataset['Owner'] = pandas.factorize(cars_dataset['Owner'])[0]

x = cars_dataset.drop('Price_Class', axis=1)
y = cars_dataset['Price_Class']
x = pandas.DataFrame(x, index = x.index, columns = x.columns)

x_train, x_test, y_train, y_test = train_test_split(x, y, test_size = 0.40, shuffle = True, stratify = None)

# K ближайших соседей
knn = KNeighborsClassifier(n_neighbors = 4).fit(x_train, y_train)
knn_predictions = pandas.Series(knn.predict(x_test))
print('Точность K ближайших соседей: ' + str(knn.score(x_test, y_test) * 100) + '%')

# Дерево решений
dtc = DecisionTreeClassifier(max_depth = 10).fit(x_train, y_train)
dtc_predictions = pandas.Series(dtc.predict(x_test))
print('Точность дерева решений: ' + str(dtc.score(x_test, y_test) * 100) + '%')

# Линейный дискриминантный анализ
lda = LinearDiscriminantAnalysis(n_components = 1).fit(x_train, y_train)
lda_predictions = pandas.Series(lda.predict(x_test))
print('Точность линейного дискриминантного анализа: ' + str(lda.score(x_test, y_test) * 100) + '%')

### Диаграммы ###

# K ближайших соседей
pylab.figure(figsize=(10,5))
pylab.subplot(1, 2, 1)
pyplot.pie(y_test.value_counts().sort_index(), labels = sorted(y_test.unique()), autopct='%1.1f%%')
pyplot.title('Тестовое распределение цен по классам')

pylab.subplot(1, 2, 2)
pyplot.pie(knn_predictions.value_counts().sort_index(), labels = sorted(knn_predictions.unique()), autopct='%1.1f%%')
pyplot.title('Предсказание методом K ближайших соседей')
pyplot.show()

# Дерево решений
pylab.figure(figsize=(10,5))
pylab.subplot(1, 2, 1)
pyplot.pie(y_test.value_counts().sort_index(), labels = sorted(y_test.unique()), autopct='%1.1f%%')
pyplot.title('Тестовое распределение цен по классам')

pylab.subplot(1, 2, 2)
pyplot.pie(dtc_predictions.value_counts().sort_index(), labels = sorted(dtc_predictions.unique()), autopct='%1.1f%%')
pyplot.title('Предсказание деревом решений')
pyplot.show()

# Линейный дискриминантный анализ
pylab.figure(figsize=(10,5))
pylab.subplot(1, 2, 1)
pyplot.pie(y_test.value_counts().sort_index(), labels = sorted(y_test.unique()), autopct='%1.1f%%')
pyplot.title('Тестовое распределение цен по классам')

pylab.subplot(1, 2, 2)
pyplot.pie(lda_predictions.value_counts().sort_index(), labels = sorted(lda_predictions.unique()), autopct='%1.1f%%')
pyplot.title('Предсказание методом линейного дискриминантного анализа')
pyplot.show()