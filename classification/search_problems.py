from copy import deepcopy
from simpleai.search.models import SearchProblem
from sklearn.cross_validation import cross_val_score


class FeatureSearchProblem(SearchProblem):

    def __init__(self, classifier, initial_state):
        super(FeatureSearchProblem, self).__init__(initial_state=initial_state)
        self._classifier = classifier

    def actions(self, state):
        samples, labels, path = state
        first_sample = samples[0]
        num_of_features = len(first_sample)
        return list(range(num_of_features))  # [0, 1, 2, ..., num_of_features-1]

    def result(self, state, action):
        samples, labels, path = state
        new_samples = deepcopy(samples)
        for sample in new_samples:
            del sample[action]
        return new_samples, labels, path + [action]

    def value(self, state):
        samples, labels, path = state
        return cross_val_score(self._classifier, samples, labels, cv=4).mean()


class SampleSearchProblem(SearchProblem):

    def __init__(self, classifier, initial_state):
        super(SampleSearchProblem, self).__init__(initial_state=initial_state)
        self._classifier = classifier

    def actions(self, state):
        samples, labels = state
        num_of_samples = len(samples)
        return list(range(num_of_samples))  # [0, 1, 2, ..., num_of_samples-1]

    def result(self, state, action):
        samples, labels = state
        new_samples = deepcopy(samples)
        new_labels = deepcopy(labels)
        del new_samples[action]
        del new_labels[action]
        return new_samples, new_labels

    def value(self, state):
        samples, labels = state
        return cross_val_score(self._classifier, samples, labels, cv=4).mean()
