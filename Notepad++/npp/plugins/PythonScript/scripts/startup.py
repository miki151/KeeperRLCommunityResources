# -*- coding: utf-8 -*-
import sys
from Npp import *
import os

# Route stderr to console early
sys.stderr = console

class ConsoleError:
    def __init__(self):
        self._console = console

    def write(self, text):
        self._console.writeError(text)

# -*- coding: utf-8 -*-
import sys
from Npp import *
import os
import xml.etree.ElementTree as ET

# -*- coding: utf-8 -*-
import sys
from Npp import *
import os
import xml.etree.ElementTree as ET

sys.stdout = console
sys.stderr = console

# ------------------- CONFIG --------------------
ROOT_FOLDER = r"..\..\Mods\KeeperRL_1.0_Release"
PROJECT_NAME = "KeeperRL_Auto"
NPP_DIR = notepad.getNppDir()
PROJECT_FILE = os.path.join(NPP_DIR, PROJECT_NAME + ".nppProj")
SESSION_FILE = os.path.join(NPP_DIR, "session.xml")
# ------------------------------------------------


def generate_project_file():
    """Create .nppProj with UTF-8-safe file entries."""
    if not os.path.isdir(ROOT_FOLDER):
        console.writeError("Folder missing: " + ROOT_FOLDER + "\n")
        return None

    root = ET.Element("Project", {"name": PROJECT_NAME})

    for dpath, dnames, fnames in os.walk(ROOT_FOLDER):
        for fname in fnames:
            full = os.path.normpath(os.path.join(dpath, fname))

            # UTF-8 safe for Python 2 + ElementTree
            full_utf8 = full.decode('utf-8', 'ignore').encode('utf-8')

            node = ET.SubElement(root, "File")
            node.set("filename", full_utf8)

    # Write XML manually to avoid Python 2 unicode bugs
    try:
        raw = ET.tostring(root, encoding='utf-8')
        with open(PROJECT_FILE, "wb") as f:
            f.write(b'<?xml version="1.0" encoding="utf-8"?>\n')
            f.write(raw)
        return PROJECT_FILE
    except Exception as e:
        console.writeError("Write error: %s\n" % e)
        return None


def inject_into_session(project_path):
    """Embed our project into Project Panel 1 in session.xml."""
    try:
        tree = ET.parse(SESSION_FILE)
        root = tree.getroot()
    except Exception as e:
        console.writeError("Cannot read session.xml: %s\n" % e)
        return False

    panels = root.find("ProjectPanels")
    if panels is None:
        panels = ET.SubElement(root, "ProjectPanels")

    panel1 = panels.find("ProjectPanel")
    if panel1 is None:
        panel1 = ET.SubElement(panels, "ProjectPanel")

    # Set project path
    panel1.set("path", project_path)

    # Save updated session file
    try:
        tree.write(SESSION_FILE, encoding="utf-8", xml_declaration=True)
        return True
    except Exception as e:
        console.writeError("Failed to update session.xml: %s\n" % e)
        return False


# ---------------------- MAIN ---------------------
console.write("Generating project...\n")

proj = generate_project_file()

if proj:
    if inject_into_session(proj):
        console.write("Project installed into session. Restart Notepad++ to load it.\n")
    else:
        console.writeError("Session update failed.\n")
else:
    console.writeError("Project generation failed.\n")
