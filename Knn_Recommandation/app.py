import numpy as np
from flask import Flask, request, jsonify, render_template
import pickle
import json
import sys
from KnnBookSuggestion import recommandationclass


app = Flask(__name__)

#model = pickle.load(open('modelmake.pkl', 'rb'))

@app.route('/recommand',methods=['POST'])
def predict():
    data = request.get_json(force=True)
    with open("KnnRecommandModel.pkl","rb") as f:
        obj = pickle.load(f)
    book_name=data['parameter']
    print(book_name)
    var = obj.recommandBook(book_name)
    str1 = []
    for i in var:
        str1.append(i)
    st={"book": str1}
    print(st["book"])

    return st


if __name__ == "__main__":
    app.run(debug=True)