import pandas as pd

file_path = './houses/melb_data.csv'
data = pd.read_csv(file_path)
print(data.describe())

# def display(value, count):
#     print(value * count)
 
# display("Łukasz", 10)