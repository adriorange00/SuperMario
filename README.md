# Editor.md

![Editor.md Logo](https://pandao.github.io/editor.md/images/logos/editormd-logo-180x180.png)

![GitHub Stars](https://img.shields.io/github/stars/pandao/editor.md.svg)
![GitHub Forks](https://img.shields.io/github/forks/pandao/editor.md.svg)
![GitHub Tag](https://img.shields.io/github/tag/pandao/editor.md.svg)
![GitHub Release](https://img.shields.io/github/release/pandao/editor.md.svg)
![GitHub Issues](https://img.shields.io/github/issues/pandao/editor.md.svg)
![Bower Version](https://img.shields.io/bower/v/editor.md.svg)

---

## Features

- Support Standard Markdown / CommonMark and GFM (GitHub Flavored Markdown).
- Full-featured: Real-time Preview, Image (cross-domain) upload, Preformatted text/Code blocks/Tables insert, Code fold, Search replace, Read-only, Themes, Multi-languages, i18n, HTML entities, Code syntax highlighting.
- Markdown Extras: Support ToC (Table of Contents), Emoji, Task lists, @Links, etc.
- Compatible with all major browsers (IE8+), Zepto.js, and iPad.
- Support identification, interpretation, filtering of HTML tags.
- Support TeX (LaTeX expressions, Based on KaTeX), Flowchart, and Sequence Diagram.
- Support AMD/CMD (Require.js & Sea.js) Module Loader, and custom/defined editor plugins.

---

## Table of Contents

- [Editor.md](#editormd)
  - [Features](#features)
  - [Headers](#headers)
    - [Underline Headers](#headers-underline)
  - [Formatting](#characters)
  - [Blockquotes](#blockquotes)
  - [Links](#links)
  - [Code Blocks](#code-blocks-multi-language--highlighting)
  - [Images](#images)
  - [Lists](#lists)
  - [Tables](#tables)
  - [HTML Entities](#html-entities)
  - [Markdown Extras](#markdown-extras)
    - [Task Lists](#gfm-task-list)
    - [Emoji](#emoji-mixed-smiley)
  - [TeX (LaTeX)](#texlatex)
  - [FlowChart](#flowchart)
  - [Sequence Diagram](#sequence-diagram)

---

## Headers

### Headers (Underline)

H1 Header (Underline)
=============

H2 Header (Underline)
-------------

### Characters

----

~~Strikethrough~~ <s>Strikethrough (when enable html tag decode.)</s>  
*Italic* _Italic_  
**Emphasis** __Emphasis__  
***Emphasis Italic*** ___Emphasis Italic___  

Superscript: X<sub>2</sub>, Subscript: O<sup>2</sup>  

**Abbreviation (HTML `abbr` tag)**

The <abbr title="Hyper Text Markup Language">HTML</abbr> specification is maintained by the <abbr title="World Wide Web Consortium">W3C</abbr>.

---

### Blockquotes

> Blockquotes

---

### Links

[Links](http://localhost/)  
[Links with title](http://localhost/ "link title")  
`<link>` : <https://github.com>  

---

### Code Blocks (multi-language) & highlighting

#### Inline code

`$ npm install marked`

#### Code Blocks (Indented style)

    <?php
        echo "Hello world!";
    ?>

```javascript
function test() {
  console.log("Hello world!");
}
```

---

### Images

![Example Image](https://pandao.github.io/editor.md/examples/images/4.jpg)

> Follow your heart.

---

### Lists

#### Unordered List

- Item A
- Item B
- Item C

#### Ordered List

1. Item A
2. Item B
3. Item C

---

### Tables

| First Header  | Second Header |
| ------------- | ------------- |
| Content Cell  | Content Cell  |
| Content Cell  | Content Cell  |

---

### HTML Entities

&copy; &amp; &trade; &reg; &yen; &euro;

---

### Markdown Extras

#### GFM Task List

- [x] Task 1
- [ ] Task 2
  - [ ] Subtask 1
  - [ ] Subtask 2

#### Emoji Mixed :smiley:

- :smiley: Example Emoji

---

### TeX (LaTeX)

$$E=mc^2$$  

$$\sin(\alpha)^{\theta}=\sum_{i=0}^{n}(x^i + \cos(f))$$  

---

### FlowChart

```flow
st=>start: Login
op=>operation: Login operation
cond=>condition: Successful Yes or No?
e=>end: To admin

st->op->cond
cond(yes)->e
cond(no)->op
```

---

### Sequence Diagram

```seq
Andrew->China: Says Hello
Note right of China: China thinks\nabout it
China-->Andrew: How are you?
Andrew->>China: I am good thanks!
```
