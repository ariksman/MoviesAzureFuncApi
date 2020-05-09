# Guide to re-create basic skeleton react app structure with webpack

## React Installation with TypeScript

``` Powershell

yarn create react-app my-movies-app-webpack --template typescript
```

## Recommended folder structure

``` Text
proj/
├─ dist/
└─ src/
   └─ components/
```

## Babel

```  Powershell
yarn add @babel/core babel-loader @babel/preset-react @babel/preset-typescript @babel/preset-env @babel/plugin-proposal-class-properties @babel/plugin-proposal-object-rest-spread --save-dev
```

Add a file in the root of the project, ```.babelrc```. It should look contain following:

``` JSON
{
  "presets": [
    "@babel/preset-env",
    "@babel/preset-typescript",
    "@babel/preset-react"
  ],
  "plugins": [
    "@babel/proposal-class-properties",
    "@babel/proposal-object-rest-spread"
  ]
}
```

## Webpack and webpack-cli

Run the following commands:

``` Powershell
yarn add webpack webpack-cli --save-dev
yarn add html-webpack-plugin --save-dev
yarn add css-loader style-loader --save-dev
```

Add support for CSS next

``` Powershell
yarn add postcss-import postcss-preset-env
yarn add postcss-loader
```

Add ```postcss.config.js``` file

``` JS
module.exports = {
    plugins: {
        'postcss-import': {},
        'postcss-preset-env': {},
    }
}
```

Add Webpack Configuration file ```webpack.config.js``` to our project:

``` JS
const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
module.exports = {
  // webpack will take the files from ./src/index
  entry: './src/index',
  // and output it into /dist as bundle.js
  output: {
    path: path.join(__dirname, '/dist'),
    filename: 'bundle.js',
    publicPath: '/'
  },
  // adding .ts and .tsx to resolve.extensions will help babel look for .ts and .tsx files to transpile
  resolve: {
    extensions: ['.ts', '.tsx', '.js']
  },
  module: {
    rules: [
      // we use babel-loader to load our jsx and tsx files
    {
      test: /\.(ts|js)x?$/,
      exclude: /node_modules/,
      use: {
        loader: 'babel-loader'
      },
    },
    // css-loader to bundle all the css files into one file and style-loader to add all the styles  inside the style tag of the document
    {
      test: /\.css$/,
      use: ['style-loader', 'css-loader', 'postcss-loader']
    },
    {
      test: /\.(png|svg|jpg|jpeg|gif|ico)$/,
      exclude: /node_modules/,
      use: ['file-loader?name=[name].[ext]'] // ?name=[name].[ext] is only necessary to preserve the original file name
    }
  ]
},
devServer: {
  historyApiFallback: true,
},
plugins: [
  new HtmlWebpackPlugin({
    template: './public/index.html',
    favicon: './public/favicon.ico'
  })
 ]
};
```

## Modify package.json scripts to use webpack

``` JSON
"scripts": {
    "start": "webpack-dev-server --mode development --open --hot",
    "bundle": "webpack",
    "test": "echo \"Error: no test specified\" && exit 1",
    "build": "webpack --mode production"
},
```

## Add webpack server

``` Powershell
yarn add webpack-dev-server --save-dev
```

## Additional useful packages

Material UI for React <https://material-ui.com/>

``` Powershell
yarn add @material-ui/core
```

``` Html
//Any font of your choice
<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap" />
<link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons" />
```

Material SVG Icons

``` Powershell
yarn add @material-ui/icons
```

Axios with typescript support

``` Powershell
yarn add axios
```

clsx is a tiny utility for constructing className strings conditionally.

``` Powershell
yarn add clsx
```

## ORIGINAL README BELOW

This project was bootstrapped with [Create React App](https://github.com/facebook/create-react-app).

## Available Scripts

In the project directory, you can run:

### `yarn start`

Runs the app in the development mode.<br />
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

The page will reload if you make edits.<br />
You will also see any lint errors in the console.

### `yarn test`

Launches the test runner in the interactive watch mode.<br />
See the section about [running tests](https://facebook.github.io/create-react-app/docs/running-tests) for more information.

### `yarn build`

Builds the app for production to the `build` folder.<br />
It correctly bundles React in production mode and optimizes the build for the best performance.

The build is minified and the filenames include the hashes.<br />
Your app is ready to be deployed!

See the section about [deployment](https://facebook.github.io/create-react-app/docs/deployment) for more information.

### `yarn eject`

**Note: this is a one-way operation. Once you `eject`, you can’t go back!**

If you aren’t satisfied with the build tool and configuration choices, you can `eject` at any time. This command will remove the single build dependency from your project.

Instead, it will copy all the configuration files and the transitive dependencies (webpack, Babel, ESLint, etc) right into your project so you have full control over them. All of the commands except `eject` will still work, but they will point to the copied scripts so you can tweak them. At this point you’re on your own.

You don’t have to ever use `eject`. The curated feature set is suitable for small and middle deployments, and you shouldn’t feel obligated to use this feature. However we understand that this tool wouldn’t be useful if you couldn’t customize it when you are ready for it.

## Learn More

You can learn more in the [Create React App documentation](https://facebook.github.io/create-react-app/docs/getting-started).

To learn React, check out the [React documentation](https://reactjs.org/).
