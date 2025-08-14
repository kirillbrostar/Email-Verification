import React, { useState, useEffect } from 'react';

interface ResendButtonProps {
    email: string;
    onResend: (email: string) => Promise<boolean>;
}

const ResendButton: React.FC<ResendButtonProps> = ({ email, onResend }) => {
    const [timer, setTimer] = useState(0);
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        if (timer <= 0) return;
        const interval = setInterval(() => setTimer(t => t - 1), 1000);
        return () => clearInterval(interval);
    }, [timer]);

    const handleClick = async () => {
        setIsLoading(true);
        const success = await onResend(email);
        setIsLoading(false);
        if (success) setTimer(60);
    };

    return (
        <button
            onClick={handleClick}
            disabled={timer > 0 || isLoading}
            className="resend-button"
        >
            {timer > 0
                ? `Отправить повторно (${timer}s)`
                : isLoading
                    ? 'Отправка...'
                    : 'Отправить код'}
        </button>
    );
};

export default ResendButton;