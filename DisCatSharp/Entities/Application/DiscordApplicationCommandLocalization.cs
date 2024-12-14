using System;
using System.Collections.Generic;
using System.Linq;

namespace DisCatSharp.Entities;

/// <summary>
///     Represents a application command localization.
/// </summary>
public sealed class DiscordApplicationCommandLocalization
{
	/// <summary>
	///     Gets valid [locales](xref:modules_application_commands_translations_reference#valid-locales) for Discord.
	/// </summary>
	internal readonly List<string> ValidLocales = ["ar", "bg", "cs", "da", "de", "el", "en-GB", "en-US", "es-419", "es-ES", "fi", "fr", "he", "hi", "hr", "hu", "id", "it", "ja", "ko", "lt", "nl", "no", "pl", "pt-BR", "ro", "ru", "sv-SE", "th", "tr", "uk", "vi", "zh-CN", "zh-TW"];

	/// <summary>
	///     Initializes a new instance of <see cref="DiscordApplicationCommandLocalization" />.
	/// </summary>
	public DiscordApplicationCommandLocalization()
	{ }

	/// <summary>
	///     Initializes a new instance of <see cref="DiscordApplicationCommandLocalization" />.
	/// </summary>
	/// <param name="localizations">Localizations.</param>
	public DiscordApplicationCommandLocalization(Dictionary<string, string>? localizations)
	{
		if (localizations == null)
			return;

		foreach (var locale in localizations.Keys.Where(locale => !this.Validate(locale)))
			throw new NotSupportedException($"The provided locale \"{locale}\" is not valid for Discord.\n" +
			                                $"Valid locales: {string.Join(", ", this.ValidLocales)}");

		this.Localizations = localizations;
	}

	/// <summary>
	///     Gets the localization dict.
	/// </summary>
	public Dictionary<string, string> Localizations { get; internal set; } = [];

	/// <summary>
	///     Adds a localization.
	/// </summary>
	/// <param name="locale">The [locale](xref:modules_application_commands_translations_reference#valid-locales) to add.</param>
	/// <param name="value">The translation to add.</param>
	public void AddLocalization(string locale, string value)
	{
		if (this.Validate(locale))
			this.Localizations.Add(locale, value);
		else
			throw new NotSupportedException($"The provided locale \"{locale}\" is not valid for Discord.\n" +
			                                $"Valid locales: {string.Join(", ", this.ValidLocales)}");
	}

	/// <summary>
	///     Removes a localization.
	/// </summary>
	/// <param name="locale">The [locale](xref:modules_application_commands_translations_reference#valid-locales) to remove.</param>
	public void RemoveLocalization(string locale)
		=> this.Localizations.Remove(locale);

	/// <summary>
	///     Gets the KVPs.
	/// </summary>
	/// <returns></returns>
	public Dictionary<string, string> GetKeyValuePairs()
		=> this.Localizations;

	/// <summary>
	///     Whether the [locale](xref:modules_application_commands_translations_reference#valid-locales) to be added is valid
	///     for Discord.
	/// </summary>
	/// <param name="lang">[Locale](xref:modules_application_commands_translations_reference#valid-locales) string.</param>
	public bool Validate(string lang)
		=> this.ValidLocales.Contains(lang);
}
