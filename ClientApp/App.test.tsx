import * as React from 'react';
import { render, screen } from '@testing-library/react';
import App from './src/App';

describe('App Component', () => {
    test('renders email verification title', () => {
        render(<App />);
        const titleElement = screen.getByText(/Email Verification/i);
        expect(titleElement).toBeInTheDocument();
    });
});