const React = require('react');
const reactDom = require('react-dom');

const { BoardData } = require('./components/board_data');
import '../styles/board.css'
reactDom.render(<BoardData />, document.getElementById("root"))