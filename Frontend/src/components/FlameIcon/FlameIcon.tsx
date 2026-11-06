interface FlameIconProps {
	size?: number;
	className?: string;
}

const FlameIcon = ({ size = 30, className }: FlameIconProps) => {
	return (
		<svg
			className={className}
			width={size}
			height={size}
			viewBox="0 0 32 36"
			fill="none"
			xmlns="http://www.w3.org/2000/svg"
			aria-hidden="true">
			<defs>
				<linearGradient
					id="flameOuter"
					x1="16"
					y1="2"
					x2="16"
					y2="34"
					gradientUnits="userSpaceOnUse">
					<stop stopColor="#FFCB45" />
					<stop offset="0.45" stopColor="#FF8A24" />
					<stop offset="1" stopColor="#F04418" />
				</linearGradient>

				<linearGradient
					id="flameInner"
					x1="16"
					y1="15"
					x2="16"
					y2="33"
					gradientUnits="userSpaceOnUse">
					<stop stopColor="#FFF4A3" />
					<stop offset="1" stopColor="#FFC247" />
				</linearGradient>
			</defs>

			<path
				d="M17 1.5C18.5 8 12.5 10.5 13.5 16
                   C9.5 14 9 10.5 9.5 8
                   C5.5 12 3 17 3 22
                   C3 29.5 8.7 34.5 16 34.5
                   C23.3 34.5 29 29.5 29 22
                   C29 15 24.5 10 22 7.5
                   C22.5 13 19.5 15 18 16
                   C19 10.5 19 5.5 17 1.5Z"
				fill="url(#flameOuter)"
			/>

			<path
				d="M16 16
                   C16.5 21 11 23 11 27
                   C11 30.8 13.2 33 16 33
                   C19.8 33 22 30.3 22 27
                   C22 23.5 19 21 18 19
                   C18 23 16 24 15 25
                   C15.5 21 16 18.5 16 16Z"
				fill="url(#flameInner)"
			/>
		</svg>
	);
};

export default FlameIcon;
