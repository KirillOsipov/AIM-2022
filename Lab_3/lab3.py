import csv
import random

import numpy as np
from numpy.random import choice
from datetime import date
import os
import pandas as ps
import matplotlib.pyplot as plt
import matplotlib.dates as mdates

def build_graphics():

    data_frame = ps.read_csv('data.csv')
    salary = data_frame["salary"]
    projects = data_frame["completed_projects"]

    plt.figure(figsize=(14, 10), dpi=80)
    plt.hlines(y=projects, xmin=0, xmax=salary, color='C0', alpha=0.4, linewidth=5)
    plt.gca().set(ylabel='Number of projects', xlabel='Salary')
    plt.title('Influence of the number of projects at salary', fontdict={'size': 20})
    plt.show()

    data=[data_frame["gender"].value_counts()["m"],data_frame["gender"].value_counts()["f"]]
    plt.pie(data, labels=["Men","Women"])
    plt.title("Male to female ratio")
    plt.ylabel("")
    plt.show()

    data=data_frame
    plt.figure(figsize=(16, 10), dpi=80)
    plt.plot_date(data["start_date"],data["salary"])
    plt.gca().xaxis.set_major_locator(mdates.AutoDateLocator())
    plt.ylabel('Salary')
    plt.xlabel('Dates')
    plt.title("Salary changes")
    plt.show()

def mode(values):

    dict={}
    for elem in values:
        if elem in dict:
            dict[elem]+=1
        else:
            dict[elem]=1
    v = list(dict.values())
    k = list(dict.keys())

    return k[v.index(max(v))]

def generate_dataset():

    dataset = [["number", "full_name", "gender", "birth_date", "start_date", "division", "position", "salary",
               "completed_projects"]]
    gender = ["m", "f"]
    male_names = ["Jack", "Carlos" "Paul", "Kenneth", "Robert", "John", "Daniel", "David", "Charles",
                  "Terry", "Richard", "Harry", "Ronald", "Mark", "Theodore"]
    female_names = ["Miriam", "Kristen", "Lucille", "Mary", "Lynn", "Donna", "Jessica", "Jean", "Kelly",
                    "Kim", "Ramona", "Dawn", "Shannon", "Jennifer", "Catherine"]
    surnames = ["Robinson", "Taylor", "Rhodes", "Aguilar", "Jackson", "Black", "Castillo", "McDaniel",
                "Massey", "Byrd", "Hicks", "Reynolds", "Ryan", "Vaughn", "Elliott", "Moore", "Davis"]
    departments = ["Mobile dev.", "Frontend dev.", "Backend dev.", "IT Steering group", "CIO", "Business IT"]
    posts = ["Head of Product", "Chief Product Officer", "Senior Product Manager", "Senior Product Owner",
             "Product Manager", "Project Manager", "Product Owner", "CTO", "CIO", "Tech Lead", "Team Lead",
             "Architect", "Head of PMO", "Chief Architect", "Project Lead"]

    for i in range(1, 1000):

        sex = random.choice(gender)
        if sex == "m":
            full_name = random.choice(male_names) + " " + random.choice(surnames)
        else:
            full_name = random.choice(female_names) + " " + random.choice(surnames)

        company_foundation_date = date(day=2, month=4, year=2020).toordinal()
        hire_date = date.fromordinal(random.randint(company_foundation_date, date.today().toordinal()))

        left_birth_date = date(day=1, month=1, year=1960).toordinal()
        right_birth_date = date(day=31, month=12, year=2000).toordinal()
        birth_date = date.fromordinal(random.randint(left_birth_date, right_birth_date))

        dep = random.choice(departments)
        post = random.choice(posts)
        salary = np.random.randint(10000, 500001)
        completed_projects = np.random.randint(1, 51)

        dataset.append([i, full_name, sex, birth_date, hire_date, dep, post, salary, completed_projects])

    for per in dataset:
        for param in per:
            print(param, end=", ")
        print("")

    with open("data.csv", mode="w", encoding='utf-8') as w_file:
        file_writer = csv.writer(w_file, delimiter=",", lineterminator="\r")
        for per in dataset:
            file_writer.writerow(per)

def numpy_stats():

    db = []

    with open('data.csv', mode="r", encoding='utf-8') as f:
        reader = csv.reader(f)
        for row in reader:
            data=row
            db.append([data[0],data[1],data[2],data[3],data[4],data[5],data[6],data[7],data[8]])

    for per in db:
        for param in per:
            print(param, end=", ")
        print("")

    db=np.array(db)

    numbers=np.array(db[:,0])
    numbers=np.delete(numbers, 0)
    numbers=[int(item) for item in numbers]

    salary=np.array(db[:,7])
    salary=np.delete(salary,0)
    salary = [int(item) for item in salary]

    projects=np.array(db[:,8])
    projects = np.delete(projects, 0)
    projects = [int(item) for item in projects]

    print(numbers)
    print("")
    print("Statistical characteristics")
    print("")
    print("Number: min="+str(np.min(numbers))+" ; max="+str(np.max(numbers))+" ; ave="+str(np.average(numbers))+" ; disp="+str(np.var(numbers))+" ; std="+str(np.std(numbers))+" ; median="+str(np.median(numbers))+" ; mode="+str(mode(numbers)))
    print("Salary: min=" + str(np.min(salary)) + " ; max=" + str(np.max(salary)) + " ; ave=" + str(np.average(salary)) + " ; disp=" + str(np.var(salary)) + " ; std=" + str(np.std(salary)) + " ; median=" + str(np.median(salary)) + " ; mode=" + str(mode(salary)))
    print("Projects: min=" + str(np.min(projects)) + " ; max=" + str(np.max(projects)) + " ; ave=" + str(np.average(projects)) + " ; disp=" + str(np.var(projects)) + " ; std=" + str(np.std(projects)) + " ; median=" + str(np.median(projects)) + " ; mode=" + str(mode(projects)))

def pandas_stats():

    data_frame = ps.read_csv('data.csv')

    numbers = data_frame["number"]
    salary = data_frame["salary"]
    projects = data_frame["completed_projects"]

    print(data_frame.to_string())
    print("")
    print("Statistics")
    print("")
    print("Number: min=" + str(numbers.min()) + " ; max=" + str(numbers.max()) + " ; ave=" + str(numbers.mean()) + " ; disp=" + str(numbers.var()) + " ; std=" + str(numbers.std()) + " ; median=" + str(numbers.median()) + " ; mode=" + str(mode(numbers)))
    print("Salary: min=" + str(salary.min()) + " ; max=" + str(salary.max()) + " ; ave=" + str(salary.mean()) + " ; disp=" + str(salary.var()) + " ; std=" + str(salary.std()) + " ; median=" + str(salary.median()) + " ; mode=" + str(mode(salary)))
    print("Projects: min=" + str(projects.min()) + " ; max=" + str(projects.max()) + " ; ave=" + str(projects.mean()) + " ; disp=" + str(projects.var()) + " ; std=" + str(projects.std()) + " ; median=" + str(projects.median()) + " ; mode=" + str(mode(projects)))

if __name__ == '__main__':

    print("========== Dataset ==========")

    print("")
    generate_dataset()
    print("")

    print("========== Numpy ==========")

    print("")
    numpy_stats()
    print("")

    print("========== Pandas ==========")

    print("")
    pandas_stats()
    print("")

    print("========== Graphics ==========")

    print("")
    build_graphics()

    os.remove("data.csv")