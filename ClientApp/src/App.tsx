import  { useState } from 'react';
import * as React from 'react';
import EmailForm from './components/EmailForm';
import CodeForm from './components/CodeForm';
import './App.css';

const App = () => {
    const [email, setEmail] = useState('');
    const [codeSent, setCodeSent] = useState(false);

    const handleCodeSent = (email: string) => {
        setEmail(email);
        setCodeSent(true);
    };

    return (
        <div className="app">
            {!codeSent ? (
                <EmailForm onCodeSent={handleCodeSent} />
            ) : (
                <CodeForm email={email} />
            )}
        </div>
    );
};

export default App;