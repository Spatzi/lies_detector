from tqdm import tqdm
from simpleai.search.local import hill_climbing_stochastic
from sklearn.cross_validation import cross_val_score
from sklearn.naive_bayes import GaussianNB
from sklearn.neighbors import KNeighborsClassifier
from sklearn.tree import DecisionTreeClassifier

from search_problems import FeatureSearchProblem
from data_utils import prepare_data


_score_iter = 100  # Iterations to calculate average score
_search_iter = 100  # Iterations of feature search


def _restore_path(path, num_of_features):
    if len(path) == 0:
        return []
    new_path = []
    restoration = [i for i in range(num_of_features)]
    for i in xrange(len(path)):
        for j in xrange(path[i], num_of_features):
            if path[i] == restoration[j]:
                restoration[j] = -1
                new_path = new_path + [j]
            elif restoration[j] > path[i]:
                restoration[j] -= 1
    return new_path


def _update_histogram(histogram, dropped_features):
    if len(dropped_features) != 0:
        for i in xrange(len(dropped_features)):
            histogram[dropped_features[i]] -= 1
    

def _get_score(classifier):
    score_sum = 0
    for _ in tqdm(range(_score_iter)):
        samples, labels = prepare_data()
        score_sum += cross_val_score(classifier, samples, labels, cv=4).mean() * 100
    return score_sum / _score_iter


def _get_score_with_optimal_features(classifier):
    score_sum = 0
    samples, labels = prepare_data()
    num_of_features = len(samples[0])
    histogram = [_score_iter for i in range(num_of_features)]
    for _ in tqdm(range(_score_iter)):
        samples, labels = prepare_data()
        search_problem = FeatureSearchProblem(classifier=classifier, initial_state=(samples, labels, []))
        samples, labels, path = hill_climbing_stochastic(search_problem, iterations_limit=_search_iter).state
        dropped_features = _restore_path(path, num_of_features)
        _update_histogram(histogram, dropped_features)
        score_sum += cross_val_score(classifier, samples, labels, cv=4).mean() * 100
    print histogram
    return score_sum / _score_iter


def report_initial_scores():
    print("Gaussian Naive Bayes: " + str(_get_score(GaussianNB())))
    print("Decision Tree (min leaf=1): " + str(_get_score(DecisionTreeClassifier(min_samples_leaf=1))))
    print("Decision Tree (min leaf=3): " + str(_get_score(DecisionTreeClassifier(min_samples_leaf=3))))
    print("Decision Tree (min leaf=5): " + str(_get_score(DecisionTreeClassifier(min_samples_leaf=5))))
    print("Decision Tree (min leaf=7): " + str(_get_score(DecisionTreeClassifier(min_samples_leaf=7))))
    print("K-Nearest Neighbors (K=1): " + str(_get_score(KNeighborsClassifier(n_neighbors=1))))
    print("K-Nearest Neighbors (K=3): " + str(_get_score(KNeighborsClassifier(n_neighbors=3))))
    print("K-Nearest Neighbors (K=5): " + str(_get_score(KNeighborsClassifier(n_neighbors=5))))
    print("K-Nearest Neighbors (K=7): " + str(_get_score(KNeighborsClassifier(n_neighbors=7))))


def report_scores_with_optimal_features():
    print("Gaussian Naive Bayes (optimal features): " + str(_get_score_with_optimal_features(GaussianNB())))
    print("Decision Tree (min leaf=1, optimal features): " + str(_get_score_with_optimal_features(DecisionTreeClassifier(min_samples_leaf=1))))
    print("Decision Tree (min leaf=3, optimal features): " + str(_get_score_with_optimal_features(DecisionTreeClassifier(min_samples_leaf=3))))
    print("Decision Tree (min leaf=5, optimal features): " + str(_get_score_with_optimal_features(DecisionTreeClassifier(min_samples_leaf=5))))
    print("Decision Tree (min leaf=7, optimal features): " + str(_get_score_with_optimal_features(DecisionTreeClassifier(min_samples_leaf=7))))
    print("K-Nearest Neighbors (K=1, optimal features): " + str(_get_score_with_optimal_features(KNeighborsClassifier(n_neighbors=1))))
    print("K-Nearest Neighbors (K=3, optimal features): " + str(_get_score_with_optimal_features(KNeighborsClassifier(n_neighbors=3))))
    print("K-Nearest Neighbors (K=5, optimal features): " + str(_get_score_with_optimal_features(KNeighborsClassifier(n_neighbors=5))))
    print("K-Nearest Neighbors (K=7, optimal features): " + str(_get_score_with_optimal_features(KNeighborsClassifier(n_neighbors=7))))
