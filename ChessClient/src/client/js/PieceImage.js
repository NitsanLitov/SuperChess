export default function PieceImage(piece) {
    const pieces = {
        bking: 'https://upload.wikimedia.org/wikipedia/commons/f/f0/Chess_kdt45.svg',
        bqueen: 'https://upload.wikimedia.org/wikipedia/commons/4/47/Chess_qdt45.svg',
        brook: 'https://upload.wikimedia.org/wikipedia/commons/f/ff/Chess_rdt45.svg',
        bbishop: 'https://upload.wikimedia.org/wikipedia/commons/9/98/Chess_bdt45.svg',
        bknight: 'https://upload.wikimedia.org/wikipedia/commons/e/ef/Chess_ndt45.svg',
        bpawn: 'https://upload.wikimedia.org/wikipedia/commons/c/c7/Chess_pdt45.svg',
        wking: 'https://upload.wikimedia.org/wikipedia/commons/4/42/Chess_klt45.svg',
        wqueen: 'https://upload.wikimedia.org/wikipedia/commons/1/15/Chess_qlt45.svg',
        wrook: 'https://upload.wikimedia.org/wikipedia/commons/7/72/Chess_rlt45.svg',
        wbishop: 'https://upload.wikimedia.org/wikipedia/commons/b/b1/Chess_blt45.svg',
        wknight: 'https://upload.wikimedia.org/wikipedia/commons/7/70/Chess_nlt45.svg',
        wpawn: 'https://upload.wikimedia.org/wikipedia/commons/4/45/Chess_plt45.svg',
    };

    return pieces[`${piece.color}${piece.type}`];
}