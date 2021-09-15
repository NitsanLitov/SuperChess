const React = require('react');
const reactDom = require('react-dom');

const { Board } = require('./components/board');
import '../styles/board.css'
reactDom.render(<Board />, document.getElementById("root"))
