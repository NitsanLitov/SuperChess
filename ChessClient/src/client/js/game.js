const React = require('react');
const reactDom = require('react-dom');

const { ChessBoard } = require('./components/chess_board');
import '../styles/board.css'
reactDom.render(<ChessBoard />, document.getElementById("root"))
