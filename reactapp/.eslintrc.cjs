module.exports = {
    root: true,
    env: { node: true, browser: true, es2022: true },
    extends: [
        'eslint:recommended',
        'plugin:react/recommended',
        'plugin:react/jsx-runtime',
        'plugin:react-hooks/recommended',
    ],
    ignorePatterns: ['dist', '.eslintrc.cjs'],
    parser: '@typescript-eslint/parser', // Use the TypeScript parser
    parserOptions: {
        ecmaVersion: 2022,
        sourceType: 'module',
    },
    settings: {
        react: {
            version: '18.2', // Automatically detect the React version
        },
    },
    plugins: ['react', 'react-refresh', 'react-hooks', '@typescript-eslint'],
    rules: {
        'react-hooks/rules-of-hooks': 'error', // Enforce rules of hooks
        'react-hooks/exhaustive-deps': 'warn', // Warn about missing dependencies in hooks
        'react-refresh/only-export-components': [
            'warn', { allowConstantExport: true },],
        '@typescript-eslint/no-unused-vars': 'warn', // Enforce no unused TypeScript variables
        // Add more rules as needed
        'no-unused-vars': 'off'
    },
};

//module.exports = {
//  root: true,
//  env: { browser: true, es2020: true },
//  extends: [
//    'eslint:recommended',
//    'plugin:react/recommended',
//    'plugin:react/jsx-runtime',
//    'plugin:react-hooks/recommended',
//  ],
//  ignorePatterns: ['dist', '.eslintrc.cjs'],
//  parserOptions: { ecmaVersion: 'latest', sourceType: 'module' },
//  settings: { react: { version: '18.2' } },
//  plugins: ['react-refresh'],
//  rules: {
//    'react-refresh/only-export-components': [
//      'warn',
//      { allowConstantExport: true },
//    ],
//  },
//}
