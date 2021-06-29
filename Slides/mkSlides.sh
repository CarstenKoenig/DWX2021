#!/bin/bash

mkdir -p output
pandoc -t revealjs --from markdown+tex_math_dollars --slide-level=2 -s -o output/slides.html talk.md -V revealjs-url=../../Reveal -V theme=beige --css="../custom.css"