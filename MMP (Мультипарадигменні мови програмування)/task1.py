# subtask 1
# Given a binary tree, write a function that returns the sum of the values of that tree.
import random

import time 

start = time.time() ## точка отсчета времени

class Node:
    def __init__(self, l=None, r=None, v=0) -> None:
        self.left = l
        self.right = r
        self.value = v


def get_tree(max=50):
    print('trrree')
    nodes = [Node(v=random.randint(-100, 100)) for i in range(max + 1)]

    tree = Node()
    for i in range(max):
        sight = random.choice(['l', 'r'])
        val = random.randint(-100, 100)
        vall = random.randint(0, max)
        if sight == 'l':
            tree = Node(l=tree, r=nodes[vall], v=val)
        else:
            tree = Node(l=nodes[vall], r=tree, v=val)
    print('tree ready')
    return tree


tree1 = get_tree(5)
tree2 = get_tree(10)
tree3 = get_tree(50)


def solution1(tree: Node, result: list = None) -> int:
    if tree is None:
        return 0
    return tree.value + solution1(tree.left) + solution1(tree.right)

# def solution1(tree: Node, result: list = None) -> int:
#     if result is None:
#         result = []
#     if tree:
#         result.append(tree.value)
#         solution1(tree.left, result)
#         solution1(tree.right, result)
#     return sum(result)

# subtask 2
# Find an area of the intersection of two rectangles.
class Rect:
    def __init__(self, x=0, y=0, w=0, h=0) -> None:
        self.x = x
        self.y = y
        self.wight = w
        self.height = h


def solution2(r1: Rect, r2: Rect):
    if r1.x <= r2.x <= r1.x + r1.wight and r1.y <= r2.y <= r1.y + r1.height:
        a = r1.x + r1.wight - r2.x
        b = r1.y + r1.height - r2.y
        return a*b
    return 0


rects = [Rect(x=random.randint(-100, 100),
              y=random.randint(-100, 100),
              w=random.randint(0, 100),
              h=random.randint(0, 100)) for i in range(20)]

r1 = rects[random.randint(0, 19)]
r2 = rects[random.randint(0, 19)]


# subtask 3
# Write an efficient function that checks whether any permutation of an input string is a palindrome.
# Note that the function is not a palindrome check

def solution3(s: str):
    a=set()
    for i in s:
        if i not in a:
            a.add(i)
        else:
            a.remove(i)
    return len(a)<=1

# def solution3(s: str):
#     a = {}
#     k = 0
#     for i in s:
#         a[i] = a.get(i, 0) + 1

#     for i in a.values():
#         if i%2 != 0:
#             k += 1
#     if k > 1:
#         return False
#     return True

# def solution3(s: str):
#     a = {}
#     k = 0
#     for i in s:
#         if i in a:
#             a[i] += 1
#         else:
#             a[i] = 1

#     for i in list(a.values()):
#         k += i % 2
#         if k > 1:
#             return False
#     return True




def solution(tree: Node, r1: Rect, r2: Rect, s: str):
    return solution1(tree), solution2(r1, r2), solution3(s)


def test_func1():
    print(solution1(tree1))
    print(solution1(tree2))
    print(solution1(tree3))

    print(solution2(r1, r2))

    assert solution3('civic') == True, "Check your implementation!"
    assert solution3('ivicc') == True, "Check your implementation!"
    assert solution3('civil') == False, "Check your implementation!"
    assert solution3('livci') == False, "Check your implementation!"

    print("Local tests for func passed!")


if __name__ == "__main__":
    test_func1()

end = time.time() - start ## собственно время работы программы

print(end) ## вывод времени