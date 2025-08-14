import { useState } from 'react';
import * as React from 'react';
import { translations, defaultLanguage } from '../constants';

interface EmailFormProps {
    onCodeSent: (email: string) => void; // Тип пропса, который ожидается
}

const EmailForm: React.FC<EmailFormProps> = ({ onCodeSent }) => { // Принимаем пропс здесь
    const [email, setEmail] = useState('');
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState('');
    const t = translations[defaultLanguage];

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setIsLoading(true);
        setError('');

        try {
            const response = await fetch('/api/auth/send-code', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ email })
            });;

            if (!response.ok) {
                throw new Error(await response.text());
            }

            // Вызываем колбэк после успешной отправки
            onCodeSent(email); // <-- Вот это важно!
        } catch (err: unknown) {
            if (err instanceof Error) {
                setError(err.message);
            }
        } finally {
            setIsLoading(false);
        }
    };
    return (
        <div className="email-form">
            <h2 className="email-verification-header">{t.emailVerification}</h2>
            <form onSubmit={handleSubmit}>
                <input
                    className="email-input"
                    type="email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    placeholder={t.emailPlaceholder}
                    required
                />
                <button
                    className="submit-button"
                    type="submit"
                    disabled={isLoading}
                >
                    {isLoading ? t.sending : t.sendCode}
                </button>
                {error && <div className="error-message">{error}</div>}
            </form>
        </div>
    );
};

export default EmailForm;