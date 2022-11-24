# Вариант 14
# Формируется матрица F следующим образом: скопировать в нее А и если в В количество чисел, 
# меньших К в нечетных столбцах больше, чем сумма чисел в четных строках, то поменять 
# местами С и Е симметрично, иначе В и Е поменять местами несимметрично. При этом матрица А 
# не меняется. После чего если определитель матрицы А больше суммы диагональных элементов матрицы F, 
# то вычисляется выражение: A-1*AT – K * F, иначе вычисляется выражение (A-1 +G-FТ)*K, где G-нижняя 
# треугольная матрица, полученная из А. Выводятся по мере формирования А, F и все матричные операции последовательно.

from math import floor, ceil
import matplotlib.pyplot as plt
import numpy

try:
    N = int(input('Input N: '))
    K = int(input('Input K: '))
    A = numpy.random.randint(-10, 11, size=(N, N))
    F = numpy.copy(A)

    print(A)
    print(F)

    countLowerK = 0
    sumEvenRows = 0

    for i in range(ceil(N / 2), ceil(N)):
        for j in range(0, ceil(N / 2)):
            if j % 2 == 1 and F[i][j] < K:
                countLowerK += 1
            if i % 2 == 0:
                sumEvenRows += F[i][j]

    if countLowerK > sumEvenRows:
        for i in range(0, ceil(N / 2)):
            for j in range(0, ceil(N / 2)):
                F[i][j], F[N - 1 - j][N - 1 - i] = F[N - 1 - j][N - 1 - i], F[i][j]
        print(F)
    else:
        for i in range(0, ceil(N / 2)):
            for j in range(floor(N / 2), N):
                F[i][j], F[floor(N / 2) + i][j] = F[floor(N / 2) + i][j], F[i][j]
        print(F)

    detA = numpy.linalg.det(A)
    print(f"Determinant of A: {detA}.")
    sumDiagonal = numpy.trace(F)
    print(f"Diagonal sum: {sumDiagonal}.")
    result = 0
    if detA > sumDiagonal:
        result = numpy.linalg.matrix_power(A, -1) * A.transpose() - K * F
        print(A.transpose())
        print(numpy.linalg.matrix_power(A, -1))
    else:
        result = (numpy.linalg.matrix_power(A, -1) + numpy.tril(A) - numpy.linalg.matrix_power(F, -1)) * K
        print(numpy.linalg.matrix_power(A, -1))
        print(numpy.tril(A))
        print(numpy.linalg.matrix_power(F, -1))
    print(f"Result: {result}.")

    plt.plot(F)
    plt.show()

    for i in range(0, N):
        for j in range(0, N):
            plt.bar(i, F[i][j])
    plt.show()

    x = numpy.arange(0, N, 1)
    f0 = F[0]
    a0 = A[0]
    labels = ["F[0]", "A[0]"]
    fig, ax = plt.subplots()
    ax.stackplot(x, f0, a0, labels=labels)
    ax.legend(loc='upper left')
    plt.show()

except:
    print('Error!')