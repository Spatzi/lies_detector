from copy import deepcopy
from csv import reader
from os import path
from random import randint

_truths_label = 1
_lies_label = 0

_truths_file = 'data/truths.csv'
_lies_file = 'data/lies.csv'


def _load_data():
    labels = [_truths_label, _lies_label]
    files = [path.join(path.dirname(__file__), _truths_file), path.join(path.dirname(__file__), _lies_file)]
    all_samples = []
    all_labels = []
    for file, label in zip(files, labels):
        with open(file, 'rt') as opened_file:
            file_samples = [[float(feature) for feature in sample] for sample in reader(opened_file)]
        file_labels = [int(label) for _ in file_samples]
        all_samples += file_samples
        all_labels += file_labels
    return all_samples, all_labels


def _shuffle_data(samples, labels):
    samples = deepcopy(samples)
    labels = deepcopy(labels)
    shuffled_samples = []
    shuffled_labels = []
    while samples:
        i = randint(0, len(samples) - 1)
        shuffled_samples.append(samples[i])
        shuffled_labels.append(labels[i])
        del samples[i]
        del labels[i]
    return shuffled_samples, shuffled_labels


def _balance_data(samples, labels):
    truths_count = 0
    lies_count = 0
    for label in labels:
        if label == _truths_label:
            truths_count += 1
        else:
            lies_count += 1
    min_count = min(truths_count, lies_count)
    new_samples = []
    new_labels = []
    new_truths_count = 0
    new_lies_count = 0
    for i in range(len(samples)):
        if new_truths_count == min_count and new_lies_count == min_count:
            break
        sample = samples[i]
        label = labels[i]
        if label == _truths_label and new_truths_count < min_count:
            new_samples.append(sample)
            new_labels.append(label)
            new_truths_count += 1
        if label == _lies_label and new_lies_count < min_count:
            new_samples.append(sample)
            new_labels.append(label)
            new_lies_count += 1
    return new_samples, new_labels


def prepare_data():
    return _shuffle_data(*_balance_data(*_shuffle_data(*_load_data())))
