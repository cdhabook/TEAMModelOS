
const path = require('path');
const webpackDevServer = require('webpack-dev-server');
const VueLoaderPlugin = require('vue-loader/lib/plugin');
const webpack = require('webpack');

module.exports = {
    mode: "development",
    entry: {
        "main":"./ClientApp/src/main.js"
    },
    output: { 
        path: path.join(__dirname, "/wwwroot"),
        filename: "[name].js",
        publicPath: "/"
    },
    devServer: {
        contentBase: "/",
        hot: true,
        hotOnly: true 
    },
    module: {
        rules: [
            { test: /\.vue$/, use: 'vue-loader' },
            { test: /\.js$/, include: /ContestApp/, use: 'babel-loader' },
            { test: /\.css$/, use: ["vue-style-loader","css-loader"] },
            { test: /\.(png|jpg|jpeg|gif|svg)$/, use: 'url-loader?limit=25000' },
            { test: /\.(eot|svg|ttf|woff|woff2)$/, loader: 'file-loader' },
            { test: /\.less$/, use: [{ loader: "style-loader" }, { loader: "css-loader" }, { loader: "less-loader", options: { javascriptEnabled: true } }] }
        ]
    },
    resolve: {
        alias: {
            "@": path.resolve(__dirname, './ClientApp/src')
        }
    },
    plugins: [
        new VueLoaderPlugin(),
        new webpack.HotModuleReplacementPlugin()
    ]
}