from __future__ import annotations
import os

import numpy as np
import pandas as pd
import pickle

from typing import Dict, Any, List, Tuple


chem = pd.DataFrame({
        "action_id": [10, 10, 10, 10, 11, 11, 11],
        "Mn": [1.0, 1.1, 2.5, 2.5, 0.2, 0.3, 1.0],
        "timestamp": [
            "11:30",
            "11:35",
            "12:00",
            "12:05",
            "23:00",
            "23:30",
            "00:10"]})
addings = pd.DataFrame({
        "action_id": [10, 10, 11],
        "Mn": [500, 100, 400],
        "timestamp": [
            "11:45",
            "12:00",
            "23:55"]})

result = pd.DataFrame({
        "action_id": [10, 11],
        "Mn_add": [600, 400],
        "Mn_befor": [1.1, 0.3],
        "Mn_after": [2.5, 1.0],
        "timestamp": [
            "11:45",
            "23:55"]})

def solution(chem, addings):
    """
    Написати програму що поверне змерджений датафрейм відповідно до прикладу:
    "action_id" - ід процессу
    "Mn_add" - масса внесеного
    "Mn_befor" - хім аналіз до внесення
    "Mn_after" - хім аналіз відразу після внесення
    "timestamp" - початок внесення речовини
    """
    merged_df = pd.merge(chem, addings, on="action_id", suffixes=("_befor", "_add"))

    merged_df["Mn_after"] = merged_df["Mn_befor"] + merged_df["Mn_add"]

    result_df = merged_df[["action_id", "Mn_add", "Mn_befor", "Mn_after", "timestamp_add"]]
    result_df = result_df.rename(columns={"timestamp_add": "timestamp"})

    result_df = result_df.groupby("action_id").first().reset_index()

    return result_df