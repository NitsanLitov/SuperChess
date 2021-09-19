require('dotenv-extended/config');
const { resolve } = require("path")
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = env => {
    const isProduction = env === 'production';
    const plugins = [
        new HtmlWebpackPlugin({
            template: './src/client/default.html',
            filename: 'index.html',
            title: "OnlineChess",
            chunks: ['game']
        })
    ];

    let devtool = 'eval-source-map';

    if (isProduction) {
        process.env.NODE_ENV = env;
        devtool = 'source-map';
    }

    return {
        entry: {
            game: './src/client/js/game'
        },
        output: {
            path: resolve(__dirname, './dist'),
            filename: '[name].[hash].js',
            chunkFilename: '[name].[chunkhash].js'
        },
        devtool,
        module: {
            rules: [{
                    test: /\.css$/,
                    use: [
                        'style-loader',
                        'css-loader'
                    ]
                },
                {
                    test: /\.(js|jsx)$/,
                    use: ['babel-loader'],
                    exclude: /node_modules/
                },
                {
                    test: /\.(jp(e)?g|ttf|eot|svg|woff(2)?)(\?[a-z0-9=&.]+)?$/,
                    use: [{
                        loader: 'file-loader',
                        options: {
                            name: '/[hash].[ext]'
                        }
                    }]
                }
            ]
        },
        plugins,
        devServer: {
            port: process.env.WEBPACK_PORT,
            historyApiFallback: true,
            hot: true,
            static: resolve(__dirname, 'dist'),
            proxy: {
                '/api': {
                    target: `http://localhost:${process.env.PORT}`
                },
                '/auth': {
                    target: `http://localhost:${process.env.PORT}`
                },
                '/ws': {
                    target: `http://localhost:${process.env.PORT}`,
                    ws: true
                }
            }
        },
    };
};