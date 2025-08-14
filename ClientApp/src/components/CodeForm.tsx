import { useState } from 'react';
import * as React from 'react';
import { translations, defaultLanguage } from '../constants';

const CodeForm = ({ email }: { email: string }) => {
    const [code, setCode] = useState('');
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState('');
    const [isVerified, setIsVerified] = useState(false);
    const t = translations[defaultLanguage];

    const handleVerify = async (e: React.FormEvent) => {
        e.preventDefault();
        setIsLoading(true);

        try {
            const response = await fetch('/api/auth/send-code', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ email })
            });

            if (!response.ok) throw new Error('Verification failed');
            setIsVerified(true);
        } catch (err: unknown) { // Явно указываем тип unknown
            // Безопасное приведение типа
            if (err instanceof Error) {
                setError(err.message);
            }
        }
        finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="code-form">
            {isVerified ? (
                <div className="success-message">
                    <h3>{t.verificationSuccess}</h3>
                    <p>{t.emailVerified}</p>
                </div>
            ) : (
                <>
                    <p>{t.codeSentTo} <strong>{email}</strong></p>
                    <form onSubmit={handleVerify}>
                        <input
                            className="code-input"
                            type="text"
                            value={code}
                            onChange={(e) => setCode(e.target.value.replace(/\D/g, ''))} // Только цифры
                            placeholder={t.codePlaceholder}
                            maxLength={6}
                            required
                        />
                        <button
                            className="verify-button"
                            type="submit"
                            disabled={isLoading || code.length < 6}
                        >
                            {isLoading ? t.verifying : t.verify}
                        </button>
                        {error && <div className="error-message">{error}</div>}
                    </form>
                </>
            )}
        </div>
    );
};

export default CodeForm;